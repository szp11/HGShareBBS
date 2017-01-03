using System.Collections.Generic;
using HGShare.Enums;

namespace HGShare.FileManager
{
    public class Dictionarys
    {
        /// <summary>
        /// 文件类型字典
        /// </summary>
        public static Dictionary<FileType, string[]> Filetypedic = new Dictionary<FileType, string[]>()
        {
            {FileType.Image,new []{"jpg","jpeg","png","gif","bmp","tiff"}},
            {FileType.Rar,new []{"rar","zip","7z"}},
            {FileType.Exe,new []{"exe","bat"}},
            {FileType.Document,new []{"doc","docx","txt","pdf","ppt","pptx","xls","xlsx"}}
        }; 
    }
}
