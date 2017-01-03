using System;
using System.Collections.Generic;
using System.Linq;

namespace EsData.Business.Paping
{
    /// <summary>
    /// 一次性产生所有分页
    /// </summary>
    public class AllPaging : IPaging
    {
        private readonly Func<IList<long>> _getallid;
        private readonly int _pageSize;
        public AllPaging(int pageSize, Func<IList<long>>  getallid)
        {
            _pageSize = pageSize;
            _getallid = getallid;
        }

        public  List<PageRange> GeneratePageQueue()
        {
            IList<long> ids = _getallid();

            double count = ids.Count;

            double page = Math.Ceiling((count / _pageSize));

            long last = ids.Last();
            var queue = new List<PageRange>();

            for (int i = 1; i <= page; i++)
            {
                long begin = ids.Skip((i - 1) * _pageSize).Take(1).FirstOrDefault();

                var endid = ids.Skip((i * _pageSize) - 1).Take(1).ToList();
                long end = endid.Count > 0 ? endid.FirstOrDefault() : last;
                queue.Add(new PageRange() { Begin = begin, End = end });
            }
            return queue;
        }
    }
    /// <summary>
    /// 分页节点
    /// </summary>
    public class PageRange
    {
        public long Begin { get; set; }

        public long End { get; set; }
    }
}
