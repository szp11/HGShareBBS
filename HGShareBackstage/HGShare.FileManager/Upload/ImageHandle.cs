using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.FileManager.Upload
{
    public class ImageHandle
    {
        /// <summary>
        /// 图片宽
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 图片高
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public Image Image { get; set; }

        public ImageHandle(Stream stream)
        {
            Image = Image.FromStream(stream);

            Init();
        }

        private void Init()
        {
            Width = Image.Width;
            Height = Image.Height;
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            Image.Dispose();
        }
    }
}
