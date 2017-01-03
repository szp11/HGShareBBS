using System.Collections.Generic;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.Business.DataSync
{
    /// <summary>
    /// 监控管理
    /// </summary>
    public class QueueMonitorManager
    {
        /// <summary>
        /// 监控
        /// </summary>
        private readonly List<QueueMonitor> _queueMonitors=new List<QueueMonitor>();
        /// <summary>
        /// 监控管理
        /// </summary>
        /// <param name="log">日志</param>
        public QueueMonitorManager(ILog log)
        {
            //所有配置
            TracMsgQueueConfigs configs = TracMsgQueueConfigHelper.DeleteTaskConfigs;

            if (configs != null && configs.Count > 0)

                foreach (var tracMsgQueueConfig in configs)
                {
                    var config = (TracMsgQueueConfig) tracMsgQueueConfig;

                    var queueMonitor = new QueueMonitor(log, config);
                   
                    _queueMonitors.Add(queueMonitor);
                }
        }
        /// <summary>
        /// 开始
        /// </summary>
        public void Start()
        {
            _queueMonitors.ForEach(n=>n.Start());
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            _queueMonitors.ForEach(n => n.Stop());
        }
    }
}
