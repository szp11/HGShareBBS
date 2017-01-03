using System;
using System.Threading;
using EsData.Business.DataIndex;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.Business.DataDelete
{
    public class DeleteManyHandle : IDelete
    {
        private readonly DeleteTaskConfig _config;
        private readonly IEsHandle _es;
        private readonly ILog _log ;
        private Thread _td;
        public DeleteManyHandle(DeleteTaskConfig config, ILog log)
        {
            _config = config;
            _log = log;
            _es = new EsHandle(config.IndexName, log,50000);
        }


        public void Begin()
        {
            _td = new Thread(Delete);
            _td.Start();
        }

        private void Delete()
        {
            while (true)
            {
                try
                {
                    _log.InfoFormat("{0}__{1}开始删除...", _config.IndexName, _config.Type);
                    var deletenum= _es.DeleteDataByWhere(_config.Type, _config.WhereScript);
                    _log.InfoFormat("{0}__{1}删除结束,共删除{2}条索引...", _config.IndexName, _config.Type, deletenum);
                }
                catch (Exception ex)
                {
                    _log.Error(string.Format("{0}__{1}删除异常...", _config.IndexName, _config.Type), ex);
                }
                Thread.Sleep(_config.Interval*1000);
            }
           
        }

        public void Stop()
        {
            if(_td!=null)
                _td.Abort();
        }
    }
}
