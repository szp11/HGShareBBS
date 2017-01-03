using System;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
namespace HGShare.Common
{
    public class ImageTools
    {
        /// <summary>
        /// Base64转图片
        /// </summary>
        /// <param name="pic"></param>
        /// <returns></returns>
        public static Image Base64ToImage(string pic)
        {
            pic=pic.Replace("data:image/jpeg;base64,", "");
            byte[] imageBytes = Convert.FromBase64String(pic);
            //读入MemoryStream对象
            var memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memoryStream.Write(imageBytes, 0, imageBytes.Length);
            //转成图片
            Image image = Image.FromStream(memoryStream);

            return image;
        }
        /// <summary>
        /// 图片等比例缩小
        /// </summary>
        /// <param name="imgSource"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <returns></returns>
        public static Bitmap GetThumbnail(Image imgSource, int destHeight, int destWidth)
        {
            int sW, sH;
            // 按比例缩放           
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth)
            {
                if ((sWidth * destHeight) > (sHeight * destWidth))
                {
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else
                {
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else
            {
                sW = sWidth;
                sH = sHeight;
            }
            var outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            // 设置画布的描绘质量         
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            g.Dispose();
            // 以下代码为保存图片时，设置压缩质量     
            var encoderParams = new EncoderParameters();
            var quality = new long[1];
            quality[0] = 100;
            var encoderParam = new EncoderParameter(Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            
            return outBmp;
        }
    }
}
