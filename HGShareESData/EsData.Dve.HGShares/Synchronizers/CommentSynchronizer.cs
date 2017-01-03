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
    public class CommentSynchronizer : SynchronizerBase<CommentVModel, CommentVModel>
    {
        public override List<CommentVModel> TSqltoTEs(List<CommentVModel> models, ILog log)
        {
            return models;
        }

        public override List<CommentVModel> GetAllList(DataChangeMsg msg, ILog log)
        {
            return new List<CommentVModel> { HGShare.DataProvider.Comments.GetComment(long.Parse(msg.PkValue)) };
        }

        public override DocumentPath<CommentVModel> GetDocumentPath(DataChangeMsg msg, ILog log)
        {
            return new DocumentPath<CommentVModel>(msg.PkValue);
        }
    }
}
