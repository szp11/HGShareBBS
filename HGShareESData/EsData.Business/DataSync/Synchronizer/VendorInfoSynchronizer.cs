using System.Collections.Generic;
using EsData.DL;
using EsData.Entity;
using EsData.Utils.Log;
using Nest;

namespace EsData.Business.DataSync.Synchronizer
{
    /// <summary>
    /// VendorInfo 同步器
    /// </summary>
    public class VendorInfoSynchronizer : SynchronizerBase<VendorInfo, VendorInfo>
    {
        public override List<VendorInfo> TSqltoTEs(List<VendorInfo> models, ILog log)
        {
             return models;
        }

        public override List<VendorInfo> GetAllList(DataChangeMsg msg, ILog log)
        {
            return VendorDl.GetVendorByVendorId(long.Parse(msg.PkValue));
        }

        public override DocumentPath<VendorInfo> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            return new DocumentPath<VendorInfo>(msg.PkValue);
        }
    }
}
