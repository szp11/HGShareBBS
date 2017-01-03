using System.Collections.Generic;
using EsData.DL;
using EsData.Entity;
using EsData.Utils.Log;
using Nest;

namespace EsData.Business.DataSync.Synchronizer
{
    /// <summary>
    /// Vendorpromotionnews 同步器
    /// </summary>
    public class VendorpromotionnewsSynchronizer : SynchronizerBase<VendorPromotionNews, VendorPromotionNews>
    {
        public override List<VendorPromotionNews> TSqltoTEs(List<VendorPromotionNews> models, ILog log)
        {
            return models;
        }

        public override List<VendorPromotionNews> GetAllList(DataChangeMsg msg, ILog log)
        {
            return DataTrackDl.GetDataChangeMsgs<VendorPromotionNews>(msg);
        }

        public override DocumentPath<VendorPromotionNews> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            return new DocumentPath<VendorPromotionNews>(msg.PkValue);
        }
    }
}
