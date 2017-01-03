using System;
using System.Collections.Generic;
using System.Linq;
using EsData.Utils.Log;
using Nest;

namespace EsData.Business.DataIndex
{
    /// <summary>
    /// es操作
    /// </summary>
    public class EsHandle : IEsHandle
    {
        public IElasticClient Client { get; private set; }
        private readonly string _indexName;
        private readonly ILog _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indexName">索引</param>
        /// <param name="log">日志</param>
        /// <param name="timeout">超时时间（毫秒）</param>
        public EsHandle(string indexName, ILog log, int timeout = 30000)
        {
            _log = log;
            _indexName = indexName.ToLower();
            Client = EsHelper.CreateElasticClient(indexName, timeout);
        }
        /// <summary>
        /// 重新创建索引
        /// </summary>
        /// <param name="replicas">副本数</param>
        /// <param name="shards">分片数</param>
        public void CreateIndex(int replicas = 1, int shards = 5)
        {

            if (Client.IndexExists(_indexName).Exists)
            {

                //删除索引
                Client.DeleteIndex(_indexName);
            }

            IIndexState indexState = new IndexState
            {
                Settings = new IndexSettings
                {
                    NumberOfReplicas = replicas,//副本数
                    NumberOfShards = shards//分片数
                }
            };
            //创建并设置
            Client.CreateIndex(_indexName, p => p.InitializeUsing(indexState));
        }
        /// <summary>
        /// 删除别名之前关系
        /// </summary>
        /// <param name="aliase"></param>
        public void DeleteAliasAndIndex(string aliase)
        {

            if (!string.IsNullOrEmpty(aliase))
            {
                aliase = aliase.ToLower();
                //该别名下所有 索引
                var result = Client.GetAlias(a => a.Name(aliase));
                if (result.Indices != null)
                {
                    var indices = result.Indices.Select(index => index.Key).Select(dummy => (IndexName)dummy).ToArray();
                    if (indices.Length > 0)
                    {
                        Client.DeleteAlias(indices, aliase);
                        //删除索引
                        Client.DeleteIndex(indices);
                    }
                }
            }
        }
        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="indexName"></param>
        public void DeleteIndex(string indexName)
        {
            //删除索引
            if (!string.IsNullOrEmpty(indexName))
                Client.DeleteIndex(indexName);
        }

        /// <summary>
        /// 将别名设置到当前索引上并删除之前关系
        /// </summary>
        /// <param name="aliase"></param>
        public void SetAlias(string aliase)
        {
            //设置别名
            if (!string.IsNullOrEmpty(aliase))
            {
                aliase = aliase.ToLower();
                //该别名下所有 索引
                var result = Client.GetAlias(a => a.Name(aliase));
                if (result.Indices != null)
                {
                    var indices = result.Indices.Select(index => index.Key).Select(dummy => (IndexName)dummy).ToArray();
                    if (indices.Length > 0)
                    {
                        Client.Alias(
                            a =>
                                a.Remove(r => r.Alias(aliase).Index(indices[0].Name))
                                    .Add(d => d.Index(_indexName).Alias(aliase)));
                        //删除索引
                        Client.DeleteIndex(indices);
                    }
                    else
                    {
                        Client.Alias(a => a.Add(d => d.Index(_indexName).Alias(aliase)));
                    }
                }
                else
                {
                    Client.Alias(a => a.Add(d => d.Index(_indexName).Alias(aliase)));
                }
            }

        }

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Mapping<T>() where T : class
        {
            var result = Client.Map<T>(m => m.AutoMap());
        }
        /// <summary>
        /// 索引数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        public void InsertData<T>(IList<T> datas) where T : class
        {
            var result = Client.IndexMany(datas);
            if (result.Errors)
            {
                _log.Error("批量索引失败:" + result.Items.Count());
                throw new Exception("批量索引含有失败的项");
            }
        }
        /// <summary>
        /// 索引数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        public bool InsertData<T>(T datas) where T : class
        {
            var result = Client.Index(datas);
            if (result.ServerError != null)
            {
                _log.Error("索引失败:" + result.ServerError.Error.Reason);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="type"></param>
        /// <param name="wherestr"></param>
        public long DeleteDataByWhere(string type, string wherestr)
        {
            IDeleteByQueryRequest request = new DeleteByQueryRequest(_indexName, type);
            request.Query = new ScriptQuery
            {
                Inline = wherestr

            };
            var result = Client.DeleteByQuery(request);

            if (result.ServerError != null)
            {
                _log.Error("删除失败:" + result.ServerError.Error.Reason);
                return 0;
            }
            if (result.Indices != null && result.Indices.ContainsKey("_all"))
                return result.Indices["_all"].Deleted;
            return 0;
        }


    }
    /// <summary>
    /// es操作
    /// </summary>
    public interface IEsHandle
    {
        IElasticClient Client { get; }


        /// <summary>
        /// 重新创建索引
        /// </summary>
        /// <param name="replicas">副本数</param>
        /// <param name="shards">分片数</param>
        void CreateIndex(int replicas = 1, int shards = 5);

        /// <summary>
        /// 删除别名之前关系
        /// </summary>
        /// <param name="aliase"></param>
        void DeleteAliasAndIndex(string aliase);
        /// <summary>
        /// 删除索引
        /// </summary>
        /// <param name="indexName"></param>
        void DeleteIndex(string indexName);

        /// <summary>
        /// 将别名设置到当前索引上并删除之前关系
        /// </summary>
        /// <param name="aliase"></param>
        void SetAlias(string aliase);

        /// <summary>
        /// 映射
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Mapping<T>() where T : class;

        /// <summary>
        /// 索引数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        void InsertData<T>(IList<T> datas) where T : class;

        /// <summary>
        /// 索引数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        bool InsertData<T>(T datas) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="type"></param>
        /// <param name="wherestr"></param>
        long DeleteDataByWhere(string type, string wherestr);
    }
}
