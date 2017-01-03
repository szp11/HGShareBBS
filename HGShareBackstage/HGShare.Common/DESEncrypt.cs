using System;
using System.Security.Cryptography;
using System.Text;

namespace HGShare.Common
{
	/// <summary>
	/// DES加密/解密类
	/// </summary>
	public class DesEncrypt
	{
	    #region ========加密========
		/// <summary> 
		/// 加密数据 
		/// </summary> 
		/// <param name="text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Encrypt(string text, string sKey)
		{
			var des = new DESCryptoServiceProvider();
		    byte[] inputByteArray = Encoding.UTF8.GetBytes(text);
			des.Key = Encoding.ASCII.GetBytes(Md5Helper.Md5(sKey).Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(Md5Helper.Md5(sKey).Substring(0, 8));
			var ms = new System.IO.MemoryStream();
			var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			var ret = new StringBuilder();
			foreach (byte b in ms.ToArray())
			{
				ret.AppendFormat("{0:X2}", b);
			}
			return ret.ToString();
		}

		#endregion

		#region ========解密========
		/// <summary> 
		/// 解密数据 
		/// </summary> 
		/// <param name="text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Decrypt(string text, string sKey)
		{
			var des = new DESCryptoServiceProvider();
		    int len = text.Length / 2;
			var inputByteArray = new byte[len];
		    int x;
		    for (x = 0; x < len; x++)
			{
			    int i = Convert.ToInt32(text.Substring(x * 2, 2), 16);
			    inputByteArray[x] = (byte)i;
			}
#pragma warning disable 618
            des.Key = Encoding.ASCII.GetBytes(Md5Helper.Md5(sKey).Substring(0, 8));
#pragma warning restore 618
            des.IV = Encoding.ASCII.GetBytes(Md5Helper.Md5(sKey).Substring(0, 8));
			var ms = new System.IO.MemoryStream();
			var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
			cs.Write(inputByteArray, 0, inputByteArray.Length);
			cs.FlushFinalBlock();
			return Encoding.UTF8.GetString(ms.ToArray());
		}

		#endregion
	}
}
