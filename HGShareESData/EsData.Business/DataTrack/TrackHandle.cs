using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EP.Base.RabbitMQClient;
using EP.Base.RabbitMQClient.Interface;
using EP.Base.RabbitMQClient.Utils;
using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;

namespace EsData.Business.DataTrack
{
    public class TrackHandle
    {
        /// <summary>
        /// 任务集合
        /// </summary>
        List<Task> _tasks;
        /// <summary>
        /// 任务配置
        /// </summary>
        private readonly TrackTaskConfigs _configs;
        /// <summary>
        /// 
        /// </summary>
        private readonly ILog _log;
        /// <summary>
        /// 
        /// </summary>
        private readonly TaskFactory _factory = new TaskFactory();
        /// <summary>
        /// 
        /// </summary>
        private List<IBus> _busses;
        /// <summary>
        /// 初始化服务在初始化过程中保留下的版本号
        /// </summary>
        private ConcurrentDictionary<string, long> _dataVersionMsgs;

        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();

        public TrackHandle(ILog log)
        {
            _log = log;
            _configs = TrackTaskConfigHelper.DeleteTaskConfigs;
        }

        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            //重置
            _tasks = new List<Task>();
            _busses=new List<IBus>();
            _dataVersionMsgs = new ConcurrentDictionary<string, long>();

            #region 数据目录
            string fileDirectory = string.Format(@"{0}\Data\", AppDomain.CurrentDomain.BaseDirectory);
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            #endregion

            if (_configs != null && _configs.Count > 0)
            {
                #region 监听补充版本信息
                var dvsrs = ConfigurationManager.AppSettings["DataVersionSupplementRabbitmqConnectionString"];
                var dvsQueue = ConfigurationManager.AppSettings["DataVersionSupplementRabbitmqQueue"];
                if (!string.IsNullOrWhiteSpace(dvsrs) && !string.IsNullOrWhiteSpace(dvsQueue))
                {
                    //rabbitmq client
                    var bus = ClientFactory.CreateBus(dvsrs, x => x.Register<IRabbitLogger>(_ => new RabbitLogger(_log)));
                    _busses.Add(bus);
                    _tasks.Add(_factory.StartNew(() =>
                            {
                                bus.Subscribe(dvsQueue, x => x.Add<DataVersionMsg>(
                                    msg =>
                                    {
                                        string key = msg.DbConnectionKey.ToLower() + msg.TableName.ToLower();
                                        if (_dataVersionMsgs.ContainsKey(key))
                                            _dataVersionMsgs[key]=msg.Version;
                                        else
                                            _dataVersionMsgs.Add(key,msg.Version);

                                    }),
                                            c => c.WithPrefetchCount(1)
                                    );
                            }));
                }
                #endregion

                #region 监听数据库变更任务
                foreach (var taskConfig in _configs)
                {
                    var config = (TrackTaskConfig)taskConfig;
                    var bus = ClientFactory.CreateBus(config.RabbitmqConnectionString, x => x.Register<IRabbitLogger>(_ => new RabbitLogger(_log)));
                    _busses.Add(bus);
                    _tasks.Add(_factory.StartNew(() => Handel(config, bus)));
                }
                #endregion
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            CancelTokenSource.Cancel();
            if (_tasks.Count > 0)
            {
                _factory.ContinueWhenAll(_tasks.ToArray(), tks =>
                {
                    _busses.ForEach(b => b.Dispose());
                    //错误信息
                    Error(tks);
                    _tasks.ForEach(n => n.Dispose());
                    CancelTokenSource.Dispose();
                }).Wait();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="bus"></param>
        private void Handel(TrackTaskConfig config, IBus bus)
        {
            while (!CancelTokenSource.IsCancellationRequested)
            {
                try
                {
                    SendMsg(config, bus);
                }
                catch (Exception ex)
                {
                   _log.Error(ex);
                }
               
                Thread.Sleep(config.Interval * 1000);
            }
        }

        private void SendMsg(TrackTaskConfig config,IBus bus)
        {
            long version=0;
            //取文件中最后版本号
            string filePath =string.Format(@"{0}\Data\{1}_{2}.txt",AppDomain.CurrentDomain.BaseDirectory,config.DbConnectionKey, config.TableName);
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(content))
                    long.TryParse(content, out version);
            }
            string dataKey = config.DbConnectionKey.ToLower() + config.TableName.ToLower();

            _log.DebugFormat("DbConnectionKey:{0};TableName:{1};Version:{2};Msg:存储版本号;", config.DbConnectionKey, config.TableName, version);
            if (_dataVersionMsgs.ContainsKey(dataKey))
            {
                if (version > _dataVersionMsgs[dataKey])
                {
                    version = _dataVersionMsgs[dataKey];
                    _log.DebugFormat("DbConnectionKey:{0};TableName:{1};Version:{2};Msg:版本号补充后变更;", config.DbConnectionKey, config.TableName, version);
                }
                _dataVersionMsgs.Remove(dataKey);
            }

            _log.DebugFormat("DbConnectionKey:{0};TableName:{1};Version:{2};Msg:开始查询;",config.DbConnectionKey, config.TableName,version);
            //查询
            var list=DL.DataTrackDl.GetDataChangeMsgs(config.DbConnectionKey, config.TableName, version, config.PkName);
            _log.InfoFormat("DbConnectionKey:{0};TableName:{1};Version:{2};Msg:查询完成;Count:{3};", config.DbConnectionKey, config.TableName, version, list == null ? 0 : list.Count);
            //发送
            if (list != null && list.Count > 0)
            {
                foreach (var dataChangeMsg in list)
                {
                    dataChangeMsg.DbConnectionKey = config.DbConnectionKey;
                    if (bus.Publish(dataChangeMsg, config.ExChange, config.RoutingKey))
                    {
                        _log.InfoFormat(
                            "DbConnectionKey:{0};TableName:{1};Type:{2};PkName:{3};PkValue:{4};Version:{5};Status:true;Msg:发送消息成功;",
                            config.DbConnectionKey, config.TableName, dataChangeMsg.Type, dataChangeMsg.PkName,
                            dataChangeMsg.PkValue, dataChangeMsg.Version);
                    }
                    else
                    {
                        _log.ErrorFormat("DbConnectionKey:{0};TableName:{1};Type:{2};PkName:{3};PkValue:{4};Version:{5};Status:false;Msg:发送消息失败;",
                           config.DbConnectionKey, config.TableName, dataChangeMsg.Type, dataChangeMsg.PkName, dataChangeMsg.PkValue, dataChangeMsg.Version);
                    }
                }
                //保存版本号到文件
                File.WriteAllText(filePath, list.Max(n => n.Version).ToString(CultureInfo.InvariantCulture));
                _log.InfoFormat("DbConnectionKey:{0};TableName:{1};Version:{2};Msg:保存新的版本号;", config.DbConnectionKey, config.TableName, list.Max(n => n.Version));
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
    }
}
