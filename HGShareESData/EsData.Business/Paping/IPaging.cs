using System.Collections.Generic;

namespace EsData.Business.Paping
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPaging
    {
        /// <summary>
        /// 得到分页集合
        /// </summary>
        /// <returns></returns>
         List<PageRange> GeneratePageQueue();
    }
}
