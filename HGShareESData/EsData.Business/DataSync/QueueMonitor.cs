using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EP.Base.RabbitMQClient;
using EP.Base.RabbitMQClient.Interface;
using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;
using Newtonsoft.Json;

namespace EsData.Business.DataSync
{
    /// <summary>
    /// 队列监控
    /// </summary>
    public class QueueMonitor
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILog _log;
        /// <summary>
        /// 跟踪配置
        /// </summary>
        private readonly TracMsgQueueConfig _config;
        /// <summary>
        /// 任务
        /// </summary>
        private List<Task> _tasks ;
        /// <summary>
        /// 任务工厂
        /// </summary>
        private readonly TaskFactory _taskFactory;
        /// <summary>
        /// rabbitmq
        /// </summary>
        private IBus _bus;
        /// <summary>
        /// 同步器容器
        /// </summary>
        private static SynchronizerContainer _synchronizerContainer;
        /// <summary>
        /// 队列监控
        /// </summary>
        /// <param name="log">日志</param>
        /// <param name="config">跟踪配置</param>
        public QueueMonitor(ILog log, TracMsgQueueConfig config)
        {
            _log = log;
            _config = config;
            _taskFactory = new TaskFactory();
            if (_synchronizerContainer==null)
                //同步器
                _synchronizerContainer = new SynchronizerContainer(_log);
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            _tasks = new List<Task>();
            if (_config != null)
            {
                //rabbitmq client
                _bus = ClientFactory.CreateBus(_config.RabbitmqConnectionString, x => x.Register<IRabbitLogger>(_ => new RabbitLogger(_log)));

                for (int i = 0; i < _config.ThreadCount; i++)
                {
                    _tasks.Add(_taskFactory.StartNew(Monitor));
                }
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            if (_tasks.Count > 0)
            {
                _bus.Dispose();
                _taskFactory.ContinueWhenAll(_tasks.ToArray(), tks =>
                {
                    Error(tks);
                }).Wait();
                _tasks.ForEach(t => t.Dispose());
            }
        }
        /// <summary>
        /// 监控
        /// </summary>
        public void Monitor()
        {
            _bus.Subscribe(_config.Queue, x => x.Add<DataChangeMsg>(
                msg =>
                {
                    _log.Info("收到变更记录:"+JsonConvert.SerializeObject(msg));
                    //处理所有影响到的索引
                    var configs= IndexSyncConfigHelper.GetIndexSyncConfigsByDbKeyAndTableName(msg.DbConnectionKey, msg.TableName);
                    foreach (var indexSyncConfig in configs)
                    {
                        //根据dbkey+tablename+indexname 得到实现
                        _synchronizerContainer.ResolveSynchronizer(msg, indexSyncConfig.IndexName).SyncData(msg, _log, indexSyncConfig);
                    }
                }
                ),
                c=>c.WithPrefetchCount((ushort)_config.WithPrefetchCount)
                );
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
