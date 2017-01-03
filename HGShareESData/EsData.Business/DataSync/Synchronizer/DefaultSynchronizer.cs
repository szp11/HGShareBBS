using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;
using Newtonsoft.Json;

namespace EsData.Business.DataSync.Synchronizer
{
    public class DefaultSynchronizer:ISynchronizer
    {
        public bool SyncData(DataChangeMsg msg, ILog log, IndexSyncConfig config)
        {
            log.Error("未实现同步方法的消息\n" + JsonConvert.SerializeObject(msg));

            return true;
        }
       
    }
}
