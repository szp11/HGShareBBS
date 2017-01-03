using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex
{
    /// <summary>
    /// 初始化任务管理
    /// </summary>
    public class InitTaskManager
    {
        private readonly IndexConfigs _configs;
        private List<Task> _tasks;
        private readonly TaskFactory _taskFactory;
        private readonly ILog _log;
        private static readonly CancellationTokenSource CancelTokenSource = new CancellationTokenSource();
        public InitTaskManager(ILog log)
        {
            _log = log;
            _configs = IndexConfigHelper.IndexConfigs;
            _taskFactory=new TaskFactory();
        }
        /// <summary>
        /// 开始执行
        /// </summary>
        public void Start()
        {
            _tasks = new List<Task>();
            if (_configs != null && _configs.Count > 0)
            {
                foreach (var indexConfig in _configs)
                {
                    var config = (IndexConfig) indexConfig;
                    _tasks.Add(_taskFactory.StartNew(() => Handle(config), CancelTokenSource.Token));
                }
            }
        }
        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            CancelTokenSource.Cancel();
            if (_tasks.Count > 0)
            {
                _taskFactory.ContinueWhenAll(_tasks.ToArray(), tks =>
                {
                    Error(tks);
                    _tasks.ForEach(n => n.Dispose());
                    CancelTokenSource.Dispose();
                });
                
            }
        }
        public void Handle(IndexConfig config)
        {
            _log.Info(config.IndexType+"开始初始化！");
            try
            {
                while (!CancelTokenSource.IsCancellationRequested)
                {
                    //开始执行
                    var dataInit = new DataInit(config.IndexType, _log);
                    dataInit.Exec();
                    Thread.Sleep(config.Interval * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                _log.Error(config.IndexType+"初始化索引数据过程中出现错误",ex);
                throw ex;
            }
        }

        /// <summary>
        /// 任务错误收集
        /// </summary>
        /// <param name="tks"></param>
        private void Error(IEnumerable<Task> tks)
        {
            foreach (var tk in tks)
            {
                if (tk.Exception != null)
                    foreach (var item in tk.Exception.InnerExceptions)
                    {
                        if (item.GetType() != typeof(ThreadAbortException))
                        {
                            _log.Error(string.Format("异常类型\t{0}\n来自\t{1}\n异常内容\t{2}\n堆栈\t{3}", item.GetType(),
                                item.Source, item.Message, item.StackTrace));
                        }
                    }
            }
        }
    }
}
