using HGShare.Enums;

namespace HGShare.VWModel
{
    /// <summary>
    /// 提示信息
    /// </summary>
    public class TipsVModel
    {
        /// <summary>
        /// 提示类型
        /// </summary>
        public TipsType type { get; set; }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool state { get; set; }
        /// <summary>
        /// 返回地址
        /// </summary>
        public string backUrl { get; set; }

        /// <summary>
        /// url参数
        /// </summary>
        public string UrlParameters
        {
            get
            {
                return string.Format("type={0}&msg={1}&state={2}",(int)type,msg,state);
            }
        }
    }
}
