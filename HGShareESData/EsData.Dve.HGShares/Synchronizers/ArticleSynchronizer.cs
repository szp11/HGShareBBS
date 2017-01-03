using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsData.Business.DataSync;
using EsData.Core;
using EsData.Utils.Log;
using HGShare.VWModel;
using Nest;

namespace EsData.Dve.HGShares.Synchronizers
{
    /// <summary>
    /// 暂未使用同步器
    /// </summary>
    public class ArticleSynchronizer : SynchronizerBase<ArticleVModel, ArticleVModel>
    {
        public override List<ArticleVModel> TSqltoTEs(List<ArticleVModel> models, ILog log)
        {
            return models;
        }

        public override List<ArticleVModel> GetAllList(DataChangeMsg msg, ILog log)
        {
            return new List<ArticleVModel> { HGShare.DataProvider.Articles.GetArticleVModel(long.Parse(msg.PkValue)) };
        }

        public override DocumentPath<ArticleVModel> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            return new DocumentPath<ArticleVModel>(msg.PkValue);
        }
    }
}
