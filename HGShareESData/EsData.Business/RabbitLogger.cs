using System;
using EP.Base.RabbitMQClient.Interface;
using EsData.Utils.Log;

namespace EsData.Business
{
    /// <summary>
    /// rabbitmq log
    /// </summary>
    public class RabbitLogger : IRabbitLogger
    {
        private readonly ILog _log;
        public RabbitLogger(ILog log)
        {
            _log = log;
        }

        public void DebugWrite(string format, params object[] args)
        {
            format = "RabbitMqLog:" + format;
            _log.DebugFormat(format,args);
            //_log.Debug(format);
        }

        public void InfoWrite(string format, params object[] args)
        {
            format = "RabbitMqLog:" + format;
            _log.InfoFormat(format, args);
            //_log.Info(format);
        }

        public void ErrorWrite(string format, params object[] args)
        {
            format = "RabbitMqLog:" + format;
            _log.ErrorFormat(format, args);
            //_log.Error(format);
        }

        public void ErrorWrite(Exception exception)
        {
             _log.Error(exception);
        }
    }
}
