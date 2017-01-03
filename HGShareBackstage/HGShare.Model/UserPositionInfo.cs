namespace HGShare.Model
{
    /// <summary>
    /// UserPosition 实体
    /// </summary>
    public class UserPositionInfo
    {
        /// <summary>
        /// 
        /// </summary>        
        public int UserId { get; set; }
        /// <summary>
        /// 位置代码
        /// </summary>        
        public int Code { get; set; }
        /// <summary>
        /// 类型 0省 1城 2区
        /// </summary>        
        public short Type { get; set; }

    }
}
