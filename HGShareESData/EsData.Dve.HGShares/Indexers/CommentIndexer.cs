using System.Collections.Generic;
using EsData.Business.DataIndex;
using EsData.Utils.Log;
using HGShare.VWModel;
namespace EsData.Dve.HGShares.Indexers
{
    public class CommentIndexer: IndexerSimpleBase<CommentVModel>
    {
        public CommentIndexer(IEsHandle es, ILog log)
            : base(es, log)
        {
        }

        protected override IList<CommentVModel> GetAllList()
        {
            return HGShare.DataProvider.Comments.GetAllData();
        }
    }
}
