using System;
namespace HGShare.Model
{
    /// <summary>
    /// Area 实体
    /// </summary>
    public class AreaInfo
    {
        /// <summary>
        /// 
        /// </summary>        
        public int Id { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public int Code { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string PinYin { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string SortPinYin { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public string Sort { get; set; }
        /// <summary>
        /// 
        /// </summary>        
        public int ParentCode { get; set; }

    }
}
