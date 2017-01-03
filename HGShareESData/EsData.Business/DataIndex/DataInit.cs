using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EP.Base.RabbitMQClient;
using EP.Base.RabbitMQClient.Interface;
using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex
{
    /// <summary>
    /// 索引处理
    /// </summary>
    public class DataInit
    {
        #region 私有
        /// <summary>
        /// 索引配置信息
        /// </summary>
        private readonly IndexConfig _indexConfig;
        /// <summary>
        /// es操作对象
        /// </summary>
        private readonly IEsHandle _es;
        /// <summary>
        /// 日志记录
        /// </summary>
        private readonly ILog _log;
        /// <summary>
        /// 索引类型
        /// </summary>
        private readonly string _indexType;
        /// <summary>
        /// 索引器容器
        /// </summary>
        private readonly IndexerContainer _indexerContainer;
        /// <summary>
        /// 初始化过程中如果有版本变更发送通知消息
        /// </summary>
        private IBus bus;

        private string tempIndexName;
        #endregion

        /// <summary>
        /// 索引处理
        /// </summary>
        /// <param name="type">索引类型</param>
        /// <param name="log"></param>
        public DataInit(string type, ILog log)
        {
            _log = log;
            _indexType = type;
            _indexConfig = IndexConfigHelper.IndexConfig(type);
            if (_indexConfig == null)
                throw new Exception(string.Format("{0}:未找到对应索引类型的配置信息！", _indexType));
            _log.Info(string.Format("{0}:开始操作索引...", type));
            tempIndexName = _indexConfig.IndexName + DateTime.Now.ToString("yyyyMMddHHmmss");
            //es操作对象
            _es = new EsHandle(tempIndexName, _log);
            //创建一个索引
            _es.CreateIndex(_indexConfig.Replicas, _indexConfig.Shards);
            try
            {
                _log.Info(string.Format("{0}:索引操作完成...", type));
                //索引器容器
                _indexerContainer = new IndexerContainer(_log);

                _log.Info(string.Format("{0}:开始索引数据...", type));
            }
            catch
            {
                //有异常删除已创建的索引
                _es.DeleteIndex(tempIndexName);
                throw;
            }
        }
        /// <summary>
        /// 执行
        /// </summary>
        public void Exec()
        {
            try
            {
                //记录一下对当前相关数据表的版本号
                var startVerstions = GetVerstions();

                int threadCount = _indexConfig.ThreadCount;
                _log.Info(string.Format("{0}:开始创建索引器...", _indexType));
                IIndexer indexer = _indexerContainer.ResolveIndexer(_indexType, _indexConfig.PageSize, _es);
                if (indexer.IsPaping)
                {
                    _log.Info(string.Format("{0}:开始索引分页数据...", _indexType));
                }
                else
                {
                    _log.Info(string.Format("{0}:开始索数据...", _indexType));
                    threadCount = 1;
                }
                Action action = indexer.HandleData;
                var st = new Stopwatch();
                st.Start();
                var factory = new TaskFactory();
                var taskArray = new Task[threadCount];
                for (Int32 i = 0; i < threadCount; i++)
                {
                    taskArray[i] = factory.StartNew(action);
                }
                factory.ContinueWhenAll(taskArray, tks =>
                {
                    //错误信息
                    if (Error(tks))
                    {
                        _log.Info(string.Format("{0}:开始切换别名：{1}\r\n", _indexType, _indexConfig.Alias));
                        //将别名切换到新索引
                        _es.SetAlias(_indexConfig.Alias);
                        _log.Info(string.Format("{0}:切换别名完成：{1}\r\n", _indexType, _indexConfig.Alias));
                        //记录一下运行结束时的版本号
                        var endVerstions = GetVerstions();
                        CheckStartEndVenstions(startVerstions, endVerstions);
                        _log.Info(string.Format("{0}:运行正常结束,耗时{1}毫秒...\r\n", _indexType, st.ElapsedMilliseconds));
                    }
                    else
                    {
                        _log.Info(string.Format("{0}:运行过程中有错误\r\n", _indexType));
                        //未成功时删除掉已创建索引
                        _es.DeleteIndex(tempIndexName);
                        _log.Info(string.Format("{0}:运行过程中有错误已结束,耗时{1}毫秒...\r\n", _indexType, st.ElapsedMilliseconds));
                    }
                    tks.ToList().ForEach(n => n.Dispose());
                }).Wait();
            }
            catch
            {
                //有异常删除已创建的索引
                _es.DeleteIndex(tempIndexName);
                throw;
            }
        }
        /// <summary>
        /// 任务错误收集
        /// </summary>
        /// <param name="tks"></param>
        private bool Error(IEnumerable<Task> tks)
        {
            bool flag = true;
            foreach (var tk in tks)
            {
                if (tk.Exception != null)
                    foreach (var item in tk.Exception.InnerExceptions)
                    {
                        if (item.GetType() != typeof(ThreadAbortException))
                        {
                            _log.Error(string.Format("异常类型\t{0}\n来自\t{1}\n异常内容\t{2}\n堆栈\t{3}", item.GetType(),
                                item.Source, item.Message, item.StackTrace));
                            flag = false;
                        }
                    }
            }
            return flag;
        }

        /// <summary>
        /// 获取数据变更版本
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, long> GetVerstions()
        {
            var kv = new Dictionary<string, long>();
            if (!string.IsNullOrWhiteSpace(_indexConfig.DBTables))
            {
                foreach (string datab in _indexConfig.DBTables.Split(';'))
                {
                    if (string.IsNullOrEmpty(datab))
                        continue;
                    string dbKey = datab.Split('|')[0];
                    string table = datab.Split('|')[1];
                    long verstion = DL.DataTrackDl.GetCurrentMaxVersion(dbKey, table);
                    _log.Info(string.Format("{0}:当前{1}版本{2}...\r\n", _indexType, datab, verstion));
                    kv.Add(datab.ToLower(), verstion);
                }
            }
            return kv;
        }

        /// <summary>
        /// 检测并发送消息
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void CheckStartEndVenstions(Dictionary<string, long> start, Dictionary<string, long> end)
        {
            try
            {
                if (string.IsNullOrEmpty(_indexConfig.DBTables))
                    return;
                var dvsrs = ConfigurationManager.AppSettings["DataVersionSupplementRabbitmqConnectionString"];
                var dvsExChange = ConfigurationManager.AppSettings["DataVersionSupplementRabbitmqExChange"];
                var dvsRoutingKey = ConfigurationManager.AppSettings["DataVersionSupplementRabbitmqRoutingKey"];

                if (string.IsNullOrEmpty(dvsrs) || string.IsNullOrEmpty(dvsExChange))
                {
                    _log.Warn(string.Format("{0}:数据初始化过程中有数据变更，单未配置变更消息队列，未发出同步消息！\r\n", _indexType));
                    return;
                }

                bus = ClientFactory.CreateBus(dvsrs);
                foreach (var e in end)
                {
                    if (e.Value <= 0)
                        continue;
                    if (start.ContainsKey(e.Key) && e.Value <= start[e.Key])
                        continue;

                    if (start.ContainsKey(e.Key))
                    {
                        //包含时发送开始
                        bus.Publish(new DataVersionMsg()
                        {
                            DbConnectionKey = e.Key.Split('|')[0],
                            TableName = e.Key.Split('|')[1],
                            Version = start[e.Key]
                        }, dvsExChange, dvsRoutingKey);

                        _log.Info(string.Format("{0}:发送{1}版本{2}至跟踪服务...\r\n", _indexType, e.Key, start[e.Key]));
                    }
                    else
                    {
                        //不包含时发送0 基本不会走这里
                        bus.Publish(new DataVersionMsg()
                        {
                            DbConnectionKey = e.Key.Split('|')[0],
                            TableName = e.Key.Split('|')[1],
                            Version = 0
                        }, dvsExChange, dvsRoutingKey);
                        _log.Info(string.Format("{0}:发送{1}版本{2}至跟踪服务...\r\n", _indexType, e.Key, 0));
                    }
                }
                bus.Dispose();
            }
            catch (Exception ex)
            {
                _log.Error("检测数据版本的时候出错", ex);
            }
        }

    }
}

