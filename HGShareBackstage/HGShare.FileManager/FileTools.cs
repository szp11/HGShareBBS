using System;
using System.IO;
using System.Linq;
using System.Web;
using HGShare.Enums;
using HGShare.Site.Config;

namespace HGShare.FileManager
{
    public class FileTools
    {
        /// <summary>
        /// 获取文件类型
        /// </summary>
        /// <returns></returns>
        public static FileType GetFileType(string exname)
        {
            foreach (var type in Dictionarys.Filetypedic)
            {
                var filetype = type.Key;
                if (type.Value.Any(n => n == exname))
                    return filetype;
            }
            return FileType.Other;
        }
        /// <summary>
        /// 获取完整路径
        /// </summary>
        /// <param name="inputPath"></param>
        /// <returns></returns>
        public static string GetFilePath(string inputPath)
        {
            string path;
            try
            {
                path = HttpContext.Current.Server.MapPath(DirectoriesConfig.UserAvatarPath);

            }
            catch
            {

                path = inputPath;
            }
            return path;
        }
    }
}
