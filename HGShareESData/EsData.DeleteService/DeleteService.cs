using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EsData.Business.DataDelete;
using EsData.Utils.Log;

namespace EsData.DeleteService
{
    public partial class DeleteService : ServiceBase
    {
        private ILog _log;
        private DeleteTaskManager _deleteTaskManager;
        public DeleteService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
           _log=new Log4Net("logger");
            try
            {
                _log.Info("服务开始启动...");
                _deleteTaskManager=new DeleteTaskManager(_log);
                _deleteTaskManager.Start();
                _log.Info("服务启动完成...");
            }
            catch (Exception ex)
            {
                _log.Error("服务启动异常...",ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                _log.Info("服务开始停止...");
                _deleteTaskManager.Stop();
                _log.Info("服务停止...");
            }
            catch (Exception ex)
            {
                _log.Error("服务停止异常...", ex);
            }
           
        }
    }
}
