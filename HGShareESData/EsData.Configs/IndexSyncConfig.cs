using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace EsData.Configs
{
    /// <summary>
    /// 跟踪任务配置
    /// </summary>
    public class IndexSyncConfigSection : ConfigurationSection
    {
        public static string SectionName
        {
            get { return "IndexSyncConfig"; }
        }
        [ConfigurationProperty("IndexSyncConfigs", IsDefaultCollection = true)]
        public IndexSyncConfigs IndexSyncConfigs
        {
            get { return (IndexSyncConfigs)this["IndexSyncConfigs"]; }
        }
    }
    [ConfigurationCollection(typeof(IndexSyncConfig), AddItemName = "IndexSyncConfig")]
    public class IndexSyncConfigs : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new IndexSyncConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IndexSyncConfig)element).ConfigKey;
        }
    }
    public class IndexSyncConfig : ConfigurationElement
    {
        /// <summary>
        /// 配置唯一key
        /// </summary>
        [ConfigurationProperty("ConfigKey")]
        public string ConfigKey
        {
            get { return (string)this["ConfigKey"]; }
            set { this["ConfigKey"] = value; }
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
        /// 数据库链接配置 key
        /// </summary>
        [ConfigurationProperty("DbConnectionKey")]
        public string DbConnectionKey
        {
            get { return (string)this["DbConnectionKey"]; }
            set { this["DbConnectionKey"] = value; }
        }
        /// <summary>
        /// 表名
        /// </summary>
        [ConfigurationProperty("TableName")]
        public string TableName
        {
            get { return (string)this["TableName"]; }
            set { this["TableName"] = value; }
        }
        /// <summary>
        /// 实现
        /// </summary>
        [ConfigurationProperty("TypeName")]
        public string TypeName
        {
            get { return (string)this["TypeName"]; }
            set { this["TypeName"] = value; }
        }

        

    }
    /// <summary>
    /// 
    /// </summary>
    public class IndexSyncConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static IndexSyncConfigs IndexSyncConfigs
        {
            get
            {
                return ((IndexSyncConfigSection)ConfigurationManager.GetSection(IndexSyncConfigSection.SectionName)).IndexSyncConfigs;
            }
        }
        /// <summary>
        /// 根据dbkey tablename获取配置
        /// </summary>
        /// <param name="dbConnectionKey"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static List<IndexSyncConfig> GetIndexSyncConfigsByDbKeyAndTableName(string dbConnectionKey, string tableName)
        {
            return
                IndexSyncConfigs.Cast<IndexSyncConfig>()
                    .Where(
                        n =>
                            n.DbConnectionKey.ToLower() == dbConnectionKey.ToLower() &&
                            n.TableName.ToLower() == tableName.ToLower())
                    .ToList();
        }
    }
}
