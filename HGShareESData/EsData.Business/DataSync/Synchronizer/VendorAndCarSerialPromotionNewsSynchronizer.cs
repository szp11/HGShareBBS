using System.Collections.Generic;
using EsData.DL;
using EsData.Entity;
using EsData.Utils.Log;
using Nest;

namespace EsData.Business.DataSync.Synchronizer
{
    public class VendorAndCarSerialPromotionNewsSynchronizer : SynchronizerBase<VendorAndCarSerialPromotionNews, VendorAndCarSerialPromotionNews>
    {
        public override List<VendorAndCarSerialPromotionNews> TSqltoTEs(List<VendorAndCarSerialPromotionNews> models, ILog log)
        {
            return models;
        }

        public override List<VendorAndCarSerialPromotionNews> GetAllList(DataChangeMsg msg, ILog log)
        {
            return DataTrackDl.GetDataChangeMsgs<VendorAndCarSerialPromotionNews>(msg);
        }

        public override DocumentPath<VendorAndCarSerialPromotionNews> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            return new DocumentPath<VendorAndCarSerialPromotionNews>(msg.PkValue);
        }
    }
}
