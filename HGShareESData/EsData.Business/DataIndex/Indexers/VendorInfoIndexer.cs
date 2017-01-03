using System.Collections.Generic;
using EsData.DL;
using EsData.Entity;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex.Indexers
{
    public class VendorInfoIndexer : IndexerSimpleBase<VendorInfo>
    {
        private readonly ILog _log;
        public VendorInfoIndexer(IEsHandle es, ILog log) : base(es, log)
        {
            _log = log;
        }

        protected override IList<VendorInfo> GetAllList()
        {
            return VendorDl.GetAllVendors();
        }
    }
}
