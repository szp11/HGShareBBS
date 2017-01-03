using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsData.Entity;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex.Indexers
{
    /// <summary>
    /// 
    /// </summary>
    public class DealerMainBrandIndexer : IndexerSimpleBase<DealerMainBrandInfo>
    {
        public DealerMainBrandIndexer(IEsHandle es, ILog log) : base(es, log)
        {
        }

        protected override IList<DealerMainBrandInfo> GetAllList()
        {
            return DL.DealerMainBrandDl.GetAllData();
        }
    }
}
