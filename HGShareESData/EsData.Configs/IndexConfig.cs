using System.Configuration;
using System.Linq;

namespace EsData.Configs
{
    
    public class IndexConfigSection : ConfigurationSection
    {
        public static string SectionName
        {
            get { return "IndexConfig"; }
        }
        [ConfigurationProperty("IndexConfigs", IsDefaultCollection = true)]
        public IndexConfigs IndexConfigs
        {
            get { return (IndexConfigs)this["IndexConfigs"]; }
        }
    }
    [ConfigurationCollection(typeof(IndexConfig), AddItemName = "IndexConfig")]
    public class IndexConfigs : ConfigurationElementCollection
    {
        
        protected override ConfigurationElement CreateNewElement()
        {
            return new IndexConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IndexConfig)element).IndexName;
        }
    }
    /// <summary>
    /// 索引信息配置
    /// </summary>
    public class IndexConfig : ConfigurationElement
    {
        /// <summary>
        /// 索引
        /// </summary>
        [ConfigurationProperty("IndexType", IsKey = true)]
        public string IndexType
        {
            get
            {
                return (string)this["IndexType"];
            }
            set { this["IndexType"] = value; }
        }
        /// <summary>
        /// 索引名
        /// </summary>
        [ConfigurationProperty("IndexName")]
        public string IndexName
        {
            get { return (string)this["IndexName"]; }
            set { this["IndexName"] = value; }
        }
        /// <summary>
        /// 索引别名
        /// </summary>
        [ConfigurationProperty("Alias")]
        public string Alias
        {
            get { return (string)this["Alias"]; }
            set { this["Alias"] = value; }
        }
        /// <summary>
        /// 副本
        /// </summary>
        [ConfigurationProperty("Replicas", DefaultValue = 1, IsRequired = false)]
        public int Replicas
        {
            get { return (int)this["Replicas"]; }
            set { this["Replicas"] = value; }
        }
        /// <summary>
        /// 分片
        /// </summary>
        [ConfigurationProperty("Shards", DefaultValue = 5, IsRequired = false)]
        public int Shards
        {
            get { return (int)this["Shards"]; }
            set { this["Shards"] = value; }
        }
        /// <summary>
        /// 每页数据
        /// </summary>
        [ConfigurationProperty("PageSize", DefaultValue = 5000, IsRequired = false)]
        public int PageSize
        {
            get { return (int)this["PageSize"]; }
            set { this["PageSize"] = value; }
        }
        /// <summary>
        /// 线程数
        /// </summary>
        [ConfigurationProperty("ThreadCount", DefaultValue = 10, IsRequired = false)]
        public int ThreadCount
        {
            get { return (int)this["ThreadCount"]; }
            set { this["ThreadCount"] = value; }
        }
        /// <summary>
        /// 实现类
        /// </summary>
        [ConfigurationProperty("TypeName")]
        public string TypeName
        {
            get { return (string)this["TypeName"]; }
            set { this["TypeName"] = value; }
        }
        /// <summary>
        /// 执行间隔（分钟）
        /// </summary>
        [ConfigurationProperty("Interval", DefaultValue = 60, IsRequired = false)]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }
        /// <summary>
        /// 需要检测的数据表
        /// </summary>
        [ConfigurationProperty("DBTables")]
        public string DBTables
        {
            get { return (string)this["DBTables"]; }
            set { this["DBTables"] = value; }
        }
    }
    /// <summary>
    /// 索引信息操作
    /// </summary>
    public class IndexConfigHelper
    {
        /// <summary>
        /// 索引索引信息配置
        /// </summary>
        public static IndexConfigs IndexConfigs
        {
            get
            {
                return ((IndexConfigSection)ConfigurationManager.GetSection(IndexConfigSection.SectionName)).IndexConfigs;
            }
        }
        /// <summary>
        /// 根据索引类型得到对应的配置
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IndexConfig IndexConfig(string type)
        {
            return IndexConfigs.Cast<IndexConfig>().FirstOrDefault(item => item.IndexType.ToLower() == type.ToLower());
        }
        /// <summary>
        /// 是否包含
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool Any(string type)
        {
            return IndexConfigs.Cast<IndexConfig>().Any(n => n.IndexType.ToLower() == type.ToLower());
        }
    }

}
