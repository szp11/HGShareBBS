using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;

namespace EsData.Business.DataSync
{
    /// <summary>
    /// 同步器
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        /// 同步数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="log"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        bool SyncData(DataChangeMsg msg,ILog log,IndexSyncConfig config);
    }
}
