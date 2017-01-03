using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using EsData.Business.Paping;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex
{
    public abstract class IndexerBase<T> : IIndexer where T : class, new()
    {
        private ConcurrentQueue<PageRange> pageQueue = new ConcurrentQueue<PageRange>();
        public IEsHandle Es = null;
        private readonly ILog _log;
        public int PageSize;
        protected IndexerBase(int pageSize, IEsHandle es, ILog log)
        {
            PageSize = pageSize;
            Es = es;
            _log = log;
            Es.Mapping<T>();
            pageQueue = GeneratePageQueue();
        }

        /// <summary>
        /// 根据id分页
        /// </summary>
        /// <returns></returns>
        private ConcurrentQueue<PageRange> GeneratePageQueue()
        {
            var paging = InitPagingDescriptor();

            var queue=new ConcurrentQueue<PageRange>();
            var list = paging.GeneratePageQueue();

            _log.Info(string.Format("分为{0}页...", list.Count));

            foreach (PageRange t in list)
            {
                queue.Enqueue(t);
            }
            return queue;
        }

        /// <summary>
        /// 初始化分页器
        /// </summary>
        /// <returns></returns>
        protected abstract IPaging InitPagingDescriptor();
        /// <summary>
        /// 根据自增id区间获取数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        protected abstract IList<T> GetList(long begin, long end);

        public bool IsPaping {
            get { return true; }
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        public void HandleData()
        {
            while (true)
            {
                PageRange pr;
                if (pageQueue.TryDequeue(out pr))
                {
                    _log.Info(string.Format("剩余页数:||||||||||{0}||||||||||", pageQueue.Count));
                    _log.Debug(string.Format("begin:{0}-end:{1}-开始获取数据...", pr.Begin, pr.End));
                    IList<T> dataList = GetList(pr.Begin, pr.End);

                    _log.Debug(string.Format("begin:{0}-end:{1}-获取到数据:{2}...", pr.Begin, pr.End,dataList==null?0: dataList.Count));
                    if (dataList != null && dataList.Count>0)
                        Es.InsertData(dataList);
                    _log.Debug(string.Format("begin:{0}-end:{1}-索引完毕...", pr.Begin, pr.End));
                    
                }
                else
                {
                    _log.Debug(string.Format("线程:{0}-索引完毕...", Thread.CurrentThread.ManagedThreadId));
                    Thread.CurrentThread.Abort();
                    break;
                }
            }
        }
    }
}
