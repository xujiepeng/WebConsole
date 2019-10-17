using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-16
 * Content: 加密解密类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 加密解密类
    /// </summary>
    public class EncryptUtil
    {        
        #region 32位 MD5 加密
        /// <summary>
        /// 32位 MD5 加密
        /// </summary>
        /// <param name="strInput">待加密的数据</param>
        /// <returns>加密后的数据</returns>
        public static string MD5Encrypt(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }

            using (MD5 md5 = MD5.Create())
            {
                //获取要加密的字段，并转化为Byte[]数组
                byte[] data = Encoding.UTF8.GetBytes(strInput);
                //加密Byte[]数组
                byte[] result = md5.ComputeHash(data);

                return BitConverter.ToString(result);
            }
        }
        #endregion

        #region DES 加密 解密
        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="strInput">待加密的数据</param>        
        /// <param name="KEY_8">秘钥(8个字符，限字母数字特殊符号)</param>
        /// <param name="IV_8">向量(8个字符，限字母数字特殊符号)</param>
        /// <returns>加密后的数据</returns>
        public static string DesEncrypt(string strInput, string KEY_8, string IV_8)
        {
            DESCryptoServiceProvider cryptoProvider = null;
            MemoryStream ms = null;
            CryptoStream cs = null;
           
            string strDesEncrypt = "";

            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }
            if (string.IsNullOrEmpty(KEY_8) || string.IsNullOrEmpty(IV_8))
            {
                return "";
            }
            if (KEY_8.Length != 8 || IV_8.Length != 8)
            {
                return "";
            }

            try
            {
                byte[] byKey = Encoding.ASCII.GetBytes(KEY_8);
                byte[] byIV = Encoding.ASCII.GetBytes(IV_8);
                byte[] inputArry = Encoding.UTF8.GetBytes(strInput);

                cryptoProvider = new DESCryptoServiceProvider();                
                ms = new MemoryStream();
                cs = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);
                cs.Write(inputArry,0, inputArry.Length);
                cs.FlushFinalBlock();

                strDesEncrypt = Convert.ToBase64String(ms.ToArray());

                return strDesEncrypt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cs != null)
                {
                    cs.Dispose();
                }

                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }

                if (cryptoProvider != null)
                {
                    cryptoProvider = null;
                }
            }

        }
        
        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="strInput">待解密的数据</param>
        /// <param name="KEY_8">秘钥(8个字符，限字母数字特殊符号)</param>
        /// <param name="IV_8">向量(8个字符，限字母数字特殊符号)</param>
        /// <returns>解密后的数据</returns>
        public static string DesDecrypt(string strInput, string KEY_8, string IV_8)
        {
            DESCryptoServiceProvider cryptoProvider = null;
            MemoryStream ms = null;
            CryptoStream cs = null;
            string strDesEncrypt = "";

            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }

            if (string.IsNullOrEmpty(KEY_8) || string.IsNullOrEmpty(IV_8))
            {
                return "";
            }

            if (KEY_8.Length != 8 || IV_8.Length != 8)
            {
                return "";
            }

            try
            {
                byte[] byKey = Encoding.ASCII.GetBytes(KEY_8);
                byte[] byIV = Encoding.ASCII.GetBytes(IV_8);
                byte[] inputArry = Convert.FromBase64String(strInput);

                cryptoProvider = new DESCryptoServiceProvider();
                ms = new MemoryStream();
                cs = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Write);
                cs.Write(inputArry, 0, inputArry.Length);
                cs.FlushFinalBlock();

                strDesEncrypt = Encoding.UTF8.GetString(ms.ToArray());
                return strDesEncrypt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cs != null)
                {
                    cs.Dispose();
                }

                if (ms != null)
                {
                    ms.Close();
                    ms.Dispose();
                }

                if (cryptoProvider != null)
                {
                    cryptoProvider = null;
                }
            }
        }
        #endregion

        #region DES 加密 解密(默认密钥)
        /// <summary>
        /// DES 加密(默认密钥)
        /// </summary>
        /// <param name="strInput">待加密的数据</param>
        /// <returns></returns>
        public static string DesEncrypt_Default(string strInput)
        {
            return DesEncrypt(strInput, "finchina", "finchina");
        }

        /// <summary>
        /// DES 解密(默认密钥)
        /// </summary>
        /// <param name="strInput">待解密的数据</param>
        /// <returns></returns>
        public static string DesDecrypt_Default(string strInput)
        {
            return DesDecrypt(strInput, "finchina", "finchina");
        }
        #endregion
    }
}
