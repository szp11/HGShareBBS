using System.Configuration;

namespace EsData.Configs
{
    /// <summary>
    /// 跟踪任务配置
    /// </summary>
    public class TrackTaskConfigSection : ConfigurationSection
    {
        public static string SectionName
        {
            get { return "TrackTaskConfig"; }
        }
        [ConfigurationProperty("TrackTaskConfigs", IsDefaultCollection = true)]
        public TrackTaskConfigs TrackTaskConfigs
        {
            get { return (TrackTaskConfigs)this["TrackTaskConfigs"]; }
        }
    }
    [ConfigurationCollection(typeof(TrackTaskConfig), AddItemName = "TrackTaskConfig")]
    public class TrackTaskConfigs : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new TrackTaskConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TrackTaskConfig)element).ConfigKey;
        }
    }
    public class TrackTaskConfig : ConfigurationElement
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
        /// 主键名
        /// </summary>
        [ConfigurationProperty("PkName")]
        public string PkName
        {
            get { return (string)this["PkName"]; }
            set { this["PkName"] = value; }
        }
        /// <summary>
        /// 扫描间隔（秒）
        /// </summary>
        [ConfigurationProperty("Interval", DefaultValue = 60, IsRequired = false)]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }

        /// <summary>
        /// 队列链接字符串
        /// </summary>
        [ConfigurationProperty("RabbitmqConnectionString")]
        public string RabbitmqConnectionString
        {
            get { return (string)this["RabbitmqConnectionString"]; }
            set { this["RabbitmqConnectionString"] = value; }
        }
        /// <summary>
        /// 交换机
        /// </summary>
        [ConfigurationProperty("ExChange")]
        public string ExChange
        {
            get { return (string)this["ExChange"]; }
            set { this["ExChange"] = value; }
        }
        /// <summary>
        /// 路由键
        /// </summary>
        [ConfigurationProperty("RoutingKey")]
        public string RoutingKey
        {
            get { return (string)this["RoutingKey"]; }
            set { this["RoutingKey"] = value; }
        }


    }
    /// <summary>
    /// 
    /// </summary>
    public class TrackTaskConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static TrackTaskConfigs DeleteTaskConfigs
        {
            get
            {
                return ((TrackTaskConfigSection)ConfigurationManager.GetSection(TrackTaskConfigSection.SectionName)).TrackTaskConfigs;
            }
        }
    }
}
