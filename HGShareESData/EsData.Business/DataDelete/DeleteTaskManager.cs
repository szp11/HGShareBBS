using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.Business.DataDelete
{
    /// <summary>
    /// 删除任务管理
    /// </summary>
    public class DeleteTaskManager
    {
        private readonly ILog _log;
        private readonly List<IDelete> _manager = new List<IDelete>();
        private readonly DeleteTaskConfigs _configs;
        public DeleteTaskManager(ILog log)
        {
            _log = log;
            _configs=DeleteTaskConfigHelper.DeleteTaskConfigs;
        }
        public void Start()
        {
            try
            {
                _log.Info("服务开始启动...");
                foreach (var deleteTaskConfig in _configs)
                {
                    var config = (DeleteTaskConfig) deleteTaskConfig;
                    var delete = new DeleteManyHandle(config, _log);
                    _manager.Add(delete);
                    delete.Begin();
                }
                _log.Info("服务启动完成...");
            }
            catch (Exception ex)
            {
                _log.Error("服务启动异常...",ex);
            }
        }
        public void Stop()
        {
            try
            {
                _log.Info("服务开始停止...");
                _manager.ForEach(n => n.Stop());
                _log.Info("服务停止...");
            }
            catch (Exception ex)
            {
                _log.Error("服务停止异常...", ex);
            }
        }
    }
}
