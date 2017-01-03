using System;
using System.Collections.Generic;
using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;
using Nest;
using Newtonsoft.Json;

namespace EsData.Business.DataSync
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SynchronizerBase<TSql,TEs>:ISynchronizer where TEs : class
    {
        private static IElasticClient EsClient;
        private readonly IndexSyncConfigs _configs;

        protected SynchronizerBase()
        {
            if (EsClient == null)
                EsClient = EsHelper.CreateElasticClient();
            _configs = IndexSyncConfigHelper.IndexSyncConfigs;
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public abstract List<TEs> TSqltoTEs(List<TSql> models, ILog log);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public abstract List<TSql> GetAllList(DataChangeMsg msg, ILog log); 
        /// <summary>
        /// 获取es文档地址
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public abstract DocumentPath<TEs> GetDocumentPath(DataChangeMsg msg,ILog log);

        /// <summary>
        /// 同步器
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="log"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public bool SyncData(DataChangeMsg msg, ILog log,IndexSyncConfig config)
        {
            string msgstr = JsonConvert.SerializeObject(msg);
            try
            {
                string indexName = config.IndexName.ToLower();

                if (msg.Type == "D")
                {
                    //删除
                    log.Info("开始删除数据:" + msgstr);
                    //var path = new DocumentPath<TEs>(msg.PkValue);
                    var path = GetDocumentPath(msg, log);
                    EsClient.Delete(path, n => n.Index(indexName));
                    log.Info("删除数据完成:" + msgstr);
                    return true;
                }

                //获取新数据
                log.Info("开始获取新数据:" + msgstr);
                //var infos = DataTrackDl.GetDataChangeMsgs<TSql>(msg);
                var infos = GetAllList(msg, log);
                //转换es数据
                var esInfos = TSqltoTEs(infos, log);

                //同步
                log.Info("开始同步数据:" + msgstr);
                if (infos != null && infos.Count > 0)
                {
                    //同步到es
                    var result = EsClient.IndexMany(esInfos, indexName);
                    if (result.ServerError != null)
                    {
                        log.Error("索引失败:" + result.ServerError.Error.Reason);
                        return false;
                    }
                    log.Info("同步数据完成:" + msgstr);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("同步数据失败:" + msgstr, ex);
            }
            return false;
        }

       
    }
}
