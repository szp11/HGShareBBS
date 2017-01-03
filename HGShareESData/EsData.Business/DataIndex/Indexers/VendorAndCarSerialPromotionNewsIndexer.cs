using System.Collections.Generic;
using EsData.Entity;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex.Indexers
{
    class VendorAndCarSerialPromotionNewsIndexer : IndexerSimpleBase<VendorAndCarSerialPromotionNews>
    {
        public VendorAndCarSerialPromotionNewsIndexer(IEsHandle es, ILog log) : base(es, log)
        {
        }

        protected override IList<VendorAndCarSerialPromotionNews> GetAllList()
        {
            return DL.VendorAndCarSerialPromotionNewsDl.GetAllVendorAndCarSerialPromotionNews();
        }
    }
}
