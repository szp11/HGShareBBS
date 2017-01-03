using System;
using System.Collections.Generic;
using System.Configuration;
using Elasticsearch.Net;
using Nest;

namespace EsData.Business
{
    /// <summary>
    /// eshelper
    /// </summary>
    public class EsHelper
    {
        /// <summary>
        /// 节点配置
        /// </summary>
        public static String NodeConfig
        {
            get { return ConfigurationManager.AppSettings["Nodes"]; }
        }
        /// <summary>
        /// 根据配置得到集合
        /// </summary>
        /// <returns></returns>
        public static List<Uri> GetAllNodes()
        {
            if (string.IsNullOrEmpty(NodeConfig))
                return null;

            string[] nodeshost = NodeConfig.Split(';');
            var nodUris = new List<Uri>();
            for (int i = 0; i < nodeshost.Length; i++)
            {
                if (!string.IsNullOrEmpty(nodeshost[i]))
                    nodUris.Add(new Uri(string.Format("http://{0}", nodeshost[i])));
            }
            return nodUris;
        }
        /// <summary>
        /// esclient
        /// </summary>
        /// <param name="indexName"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IElasticClient CreateElasticClient(string indexName = "", int timeout = 30000)
        {
            //var node = new Uri("http://192.168.87.13:9200");
            //var node = new Uri("http://localhost:9200");
            //基本设置
            //var settings = new ConnectionSettings(node).DefaultIndex(_indexName);
            //指定某种类型对应某个索引
            //var settings = new ConnectionSettings(node).MapDefaultTypeIndices(m => m.Add(typeof(MyClass),"test-2").Add(typeof(VendorPriceInfo),"test-3"));

            //多节点
            var nodes = GetAllNodes();

            if (nodes == null || nodes.Count == 0)
                throw new Exception("未配置ES节点!(Nodes)");

            //链接池
            //对单节点请求
            //IConnectionPool pool = new SingleNodeConnectionPool(node);
            //请求时随机请求各个正常节点，不请求异常节点,异常节点恢复后会重新被请求
            IConnectionPool pool = new StaticConnectionPool(nodes);

            //IConnectionPool pool = new SniffingConnectionPool(urls);
            //false.创建客户端时，随机选择一个节点作为客户端的请求对象，该节点异常后不会切换其它节点
            //true，请求时随机请求各个正常节点，不请求异常节点,但异常节点恢复后不会重新被请求
            //pool.SniffedOnStartup = true;

            //IConnectionPool pool = new StickyConnectionPool(urls);
            //创建客户端时，选择第一个节点作为请求主节点，该节点异常后会切换其它节点，待主节点恢复后会自动切换回来

            var settings = new ConnectionSettings(pool);
            if(!string.IsNullOrEmpty(indexName))
                settings.DefaultIndex(indexName);
            settings.RequestTimeout(new TimeSpan(timeout * 10000));

            return new ElasticClient(settings);


        }
        /// <summary>
        /// esclient
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static IElasticClient CreateElasticClient(int timeout = 30000)
        {
           return CreateElasticClient(null, timeout);
        }
        /// <summary>
        /// esclient
        /// </summary>
        /// <returns></returns>
        public static IElasticClient CreateElasticClient()
        {
          return  CreateElasticClient("");
        }
    }
}
