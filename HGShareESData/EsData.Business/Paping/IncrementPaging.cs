using System;
using System.Collections.Generic;

namespace EsData.Business.Paping
{
    /// <summary>
    /// 循环查询得到分页
    /// </summary>
    public class IncrementPaging:IPaging
    {
        private readonly Func<int, long, PageRange> _getLastMaxId;
        private readonly int _pageSize;
        public IncrementPaging(int pageSize, Func<int, long, PageRange> getLastMaxId)
        {
            _pageSize = pageSize;
            _getLastMaxId = getLastMaxId;
        }

        public  List<PageRange> GeneratePageQueue()
        {
            var pageRanges=new List<PageRange>();
            long end = 0;
            while (true)
            {
                var temp = _getLastMaxId(_pageSize, end);
                if(temp==null)
                    break;
                pageRanges.Add(temp);
                end = temp.End;
            }
            return pageRanges;
        }
    }
}
