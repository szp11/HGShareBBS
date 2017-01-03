using System;
using System.ServiceProcess;
using EsData.Business.DataSync;
using EsData.Utils.Log;

namespace EsData.SyncService
{
    public partial class SyncService : ServiceBase
    {
        private ILog _log;
        private QueueMonitorManager _queueMonitorManager;
        public SyncService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _log = new Log4Net("logger");

            _log.Info("服务开启启动...");
            try
            {
                _queueMonitorManager = new QueueMonitorManager(_log);
                _queueMonitorManager.Start();
            }
            catch (Exception ex)
            {
                _log.Error("服务异常", ex);
            }
            _log.Info("服务启动成功...");
        }

        protected override void OnStop()
        {
            try
            {
                _log.Info("服务开始停止...");
                _queueMonitorManager.Stop();
                _log.Info("服务停止成功...");
            }
            catch (Exception ex)
            {
                _log.Error("服务停止异常", ex);
            }


        }
    }
}
