using System.Configuration;

namespace EsData.Configs
{
    
    public class DeleteTaskConfigSection : ConfigurationSection
    {
        public static string SectionName
        {
            get { return "DeleteTaskConfig"; }
        }
        [ConfigurationProperty("DeleteTaskConfigs", IsDefaultCollection = true)]
        public DeleteTaskConfigs DeleteTaskConfigs
        {
            get { return (DeleteTaskConfigs)this["DeleteTaskConfigs"]; }
        }

    }
    [ConfigurationCollection(typeof(DeleteTaskConfig), AddItemName = "DeleteTaskConfig")]
    public class DeleteTaskConfigs : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new DeleteTaskConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((DeleteTaskConfig)element).IndexName;
        }
    }
    /// <summary>
    /// 删除任务配置
    /// </summary>
    public class DeleteTaskConfig : ConfigurationElement
    {
        
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
        /// 类型
        /// </summary>
        [ConfigurationProperty("Type")]
        public string Type
        {
            get { return (string)this["Type"]; }
            set { this["Type"] = value; }
        }
        /// <summary>
        /// 依据条件脚本
        /// </summary>
        [ConfigurationProperty("WhereScript")]
        public string WhereScript
        {
            get { return (string)this["WhereScript"]; }
            set { this["WhereScript"] = value; }
        }
        /// <summary>
        /// 间隔(秒)
        /// </summary>
        [ConfigurationProperty("Interval", DefaultValue = 10, IsRequired = false)]
        public int Interval
        {
            get { return (int)this["Interval"]; }
            set { this["Interval"] = value; }
        }
        
    }

    /// <summary>
    /// 
    /// </summary>
    public class DeleteTaskConfigHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static DeleteTaskConfigs DeleteTaskConfigs
        {
            get
            {
                return ((DeleteTaskConfigSection)ConfigurationManager.GetSection(DeleteTaskConfigSection.SectionName)).DeleteTaskConfigs;
            }
        }
    }
}
