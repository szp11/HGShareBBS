using System.Configuration;

namespace EsData.Configs
{
    /// <summary>
    /// 跟踪任务配置
    /// </summary>
    public class TracMsgQueueConfigSection : ConfigurationSection
    {
        public static string SectionName
        {
            get { return "TracMsgQueueConfig"; }
        }
        [ConfigurationProperty("TracMsgQueueConfigs", IsDefaultCollection = true)]
        public TracMsgQueueConfigs TracMsgQueueConfigs
        {
            get { return (TracMsgQueueConfigs)this["TracMsgQueueConfigs"]; }
        }
    }
    [ConfigurationCollection(typeof(TracMsgQueueConfig), AddItemName = "TracMsgQueueConfig")]
    public class TracMsgQueueConfigs : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new TracMsgQueueConfig();
        }



        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((TracMsgQueueConfig)element).ConfigKey;
        }
    }
    public class TracMsgQueueConfig : ConfigurationElement
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
        /// 线程数
        /// </summary>
        [ConfigurationProperty("ThreadCount", DefaultValue = 1, IsRequired = false)]
        public int ThreadCount
        {
            get { return (int)this["ThreadCount"]; }
            set { this["ThreadCount"] = value; }
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
        /// <summary>
        /// 队列
        /// </summary>
        [ConfigurationProperty("Queue")]
        public string Queue
        {
            get { return (string)this["Queue"]; }
            set { this["Queue"] = value; }
        }
         /// <summary>
        /// 消息预取数
        /// </summary>
        [ConfigurationProperty("WithPrefetchCount", DefaultValue = 10, IsRequired = false)]
        public int WithPrefetchCount
        {
            get { return (int)this["WithPrefetchCount"]; }
            set { this["WithPrefetchCount"] = value; }
        }

        
    }
    /// <summary>
    /// 
    /// </summary>
    public class TracMsgQueueConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static TracMsgQueueConfigs DeleteTaskConfigs
        {
            get
            {
                return ((TracMsgQueueConfigSection)ConfigurationManager.GetSection(TracMsgQueueConfigSection.SectionName)).TracMsgQueueConfigs;
            }
        }
    }
}
