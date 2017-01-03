namespace EsData.Core
{
    public class DataChangeMsg
    {
        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
        /// <summary>
        /// 数据库链接字符串配置key
        /// </summary>
        public string DbConnectionKey { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 变更类型(I U D)
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 主键名
        /// </summary>
        public string PkName { get; set; }
        /// <summary>
        /// 主键值
        /// </summary>
        public string PkValue { get; set; }
    }
}
