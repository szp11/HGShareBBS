using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsData.Entity;
using EsData.Utils.Log;
using Nest;

namespace EsData.Business.DataSync.Synchronizer
{
    /// <summary>
    /// 暂未使用同步器
    /// </summary>
    public class DealerMainBrandSynchronizer : SynchronizerBase<DealerMainBrandInfo, DealerMainBrandInfo>
    {
        public override List<DealerMainBrandInfo> TSqltoTEs(List<DealerMainBrandInfo> models, ILog log)
        {
            return models;
        }

        public override List<DealerMainBrandInfo> GetAllList(DataChangeMsg msg, ILog log)
        {
            throw new NotImplementedException();
        }

        public override DocumentPath<DealerMainBrandInfo> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            throw new NotImplementedException();
        }
    }
}
