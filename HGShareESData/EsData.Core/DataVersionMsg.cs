namespace EsData.Core
{
    /// <summary>
    /// 初始化服务中向跟踪服务中追加版本号消息
    /// </summary>
    public class DataVersionMsg
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
    }
}
