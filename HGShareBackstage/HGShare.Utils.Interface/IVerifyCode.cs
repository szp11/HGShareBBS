namespace HGShare.Utils.Interface
{
    /// <summary>
    /// 验证码 接口
    /// </summary>
    public interface IVerifyCode
    {
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        byte[] GetVerifyCode();

        /// <summary>
        /// 校验验证码
        /// </summary>
        /// <returns></returns>
        bool CheckVerifyCode(string code);
    }
}
