using System.Collections.Generic;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex
{
    /// <summary>
    /// 小数据量简单一次索引
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IndexerSimpleBase<T>: IIndexer where T : class
    {
        public IEsHandle Es = null;
        private readonly ILog _log;
        protected IndexerSimpleBase(IEsHandle es, ILog log)
        {
            Es = es;
            _log = log;
            Es.Mapping<T>();
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        protected abstract IList<T> GetAllList();

        public bool IsPaping
        {
            get { return false; }
        }

        /// <summary>
        /// 处理
        /// </summary>
        public void HandleData()
        {
            _log.Debug(string.Format("开始获取数据..."));
            IList<T> dataList = GetAllList();
            _log.Debug(string.Format("获取到数据:{0}...开始索引", dataList == null ? 0 : dataList.Count));
            if (dataList != null && dataList.Count > 0)
                Es.InsertData(dataList);
            _log.Debug("索引完毕...");
        }
    }
}
