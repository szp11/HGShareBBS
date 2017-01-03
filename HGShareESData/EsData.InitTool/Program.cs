using System;
using EsData.Business.DataIndex;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.InitTool
{
    class Program
    {
        private static readonly ILog Log = new Log4Net("logger");
        static void Main(string[] args)
        {
            string cmd = string.Empty;
            if (args.Length > 0)
                cmd = args[0];
            while (true)
            {
                string indexType;
                #region input
                while (true)
                {
                    if (string.IsNullOrEmpty(cmd))
                    {
                        CmdTips();
                        Log.Info("输入索引类型!");
                        cmd = Console.ReadLine();
                        if (IndexConfigHelper.Any(cmd))
                        {
                            indexType = cmd;
                            break;
                        }
                        cmd = string.Empty;
                        continue;
                    }
                    if (IndexConfigHelper.Any(cmd))
                    {
                        indexType = cmd;
                        break;
                    }
                }
                #endregion
                Log.Info("============================================\n");
                Log.InfoFormat("开始处理:{0}...", indexType);
                try
                {
                    //创建索引处理对象
                    var indexHandle = new DataInit(indexType, Log);
                    //执行处理
                    indexHandle.Exec();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                cmd = string.Empty;

                Log.Info("============================================\n");
            }
        }

        private static void CmdTips()
        {
            IndexConfigs configs = IndexConfigHelper.IndexConfigs;
            Console.WriteLine("命令\t索引名");
            foreach (var indexConfig in configs)
            {
                var config = (IndexConfig)indexConfig;
                Console.WriteLine(string.Format("{0}\t{1}", config.IndexType, config.Alias));
            }
            Console.WriteLine();
        }
    }
}
