using System.Collections.Generic;
using EsData.DL;
using EsData.Entity;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex.Indexers
{
    /// <summary>
    /// 经销商促销新闻索引
    /// </summary>
    public class VendorPromotionNewsIndexer : IndexerSimpleBase<VendorPromotionNews>
    {
        private readonly ILog _log;
        public VendorPromotionNewsIndexer(IEsHandle es, ILog log)
            : base(es, log)
        {
            _log = log;
        }
        protected override IList<VendorPromotionNews> GetAllList()
        {
            return VendorPromotionNewsDl.GetAllVendorPromotionNews();
        }
    }
}
