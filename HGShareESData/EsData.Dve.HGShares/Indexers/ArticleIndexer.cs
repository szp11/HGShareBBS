using System.Collections.Generic;
using EsData.Business.DataIndex;
using EsData.Utils.Log;
using HGShare.VWModel;
namespace EsData.Dve.HGShares.Indexers
{
    public class ArticleIndexer: IndexerSimpleBase<ArticleVModel>
    {
        public ArticleIndexer(IEsHandle es, ILog log)
            : base(es, log)
        {
        }

        protected override IList<ArticleVModel> GetAllList()
        {
            return HGShare.DataProvider.Articles.GetAllData();
        }
    }
}
