using System;
using System.Text;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-16
 * Content: 数据类型转换类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 数据类型转换类
    /// </summary>
    public class ConvertUtil
    {
        #region ParseString
        /// <summary>
        /// 将 Object 类型转换为 String 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 String</returns>
        public static string ParseString(object objValue, string defaultValue)
        {
            string returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                returnValue = objValue.ToString();
            }

            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = defaultValue;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 String 型,转换失败时返回 ""
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 String</returns>
        public static string ParseString(object objValue)
        {
            string returnValue = "";

            if (objValue != null && objValue != DBNull.Value)
            {
                returnValue = objValue.ToString();
            }

            return returnValue;
        }

        /// <summary>
        /// 将 byte[] 类型转换为 String 型,转换失败时返回 ""
        /// </summary>
        /// <param name="objValue">待转换 byte[] 类型</param>      
        /// <returns>返回 String</returns>
        public static string ParseString(byte[] objValue)
        {
            string returnValue = "";

            if (objValue != null)
            {
                try
                {
                    returnValue = System.Text.Encoding.UTF8.GetString(objValue);
                }
                catch
                {
                    returnValue = "";
                }
            }

            return returnValue;
        }

        #endregion

        #region ParseBool
        /// <summary>
        /// 将 String 类型转换为 bool 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 bool</returns>
        public static bool ParseBool(string strValue, bool defaultValue)
        {
            bool returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!bool.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 bool 型,转换失败时返回 false
        /// </summary>
        /// <param name="strValue">待转换 String</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(string strValue)
        {
            bool returnValue = false;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!bool.TryParse(strValue, out returnValue))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 bool 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 bool</returns>
        public static bool ParseBool(object objValue, bool defaultValue)
        {
            bool returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!bool.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 bool 型,转换失败时返回 false
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(object objValue)
        {
            bool returnValue = false;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!bool.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = false;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Byte 类型转换为 bool 型,转换失败时返回 false (小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="byteValue">待转换 Byte</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(byte byteValue)
        {
            bool returnValue = false;

            if (byteValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 short 类型转换为 bool 型,转换失败时返回 false (小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="shortValue">待转换 short</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(short shortValue)
        {
            bool returnValue = false;

            if (shortValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 int 类型转换为 bool 型,转换失败时返回 false ( 小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="intValue">待转换 int</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(int intValue)
        {
            bool returnValue = false;

            if (intValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 long 类型转换为 bool 型,转换失败时返回 false ( 小于或等于0 -- False; 大于0 -- True)
        /// </summary>
        /// <param name="longValue">待转换 long</param>      
        /// <returns>返回 bool</returns>
        public static bool ParseBool(long longValue)
        {
            bool returnValue = false;

            if (longValue > 0)
            {
                returnValue = true;
            }

            return returnValue;
        }

        #endregion

        #region ParseByte (byte : 0 - 255; 1 字节)
        /// <summary>
        /// 将 String 类型转换为 byte 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 byte</returns>
        public static byte ParseByte(string strValue, byte defaultValue)
        {
            byte returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!byte.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 byte 型,转换失败返回 byte.MinValue
        /// </summary>
        /// <param name="strValue">待转 String</param>
        /// <returns>返回 byte</returns>
        public static byte ParseByte(string strValue)
        {
            byte returnValue = byte.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!byte.TryParse(strValue, out returnValue))
                {
                    returnValue = byte.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 byte 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 byte</returns>
        public static byte ParseByte(object objValue, byte defaultValue)
        {
            byte returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!byte.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 byte 型,转换失败时返回 byte.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 byte</returns>
        public static byte ParseByte(object objValue)
        {
            byte returnValue = byte.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!byte.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = byte.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 bool 类型转换为 byte 型 (true--1;false--0)
        /// </summary>
        /// <param name="boolValue">待转换 bool</param>      
        /// <returns>返回 byte</returns>
        public static byte ParseByte(bool boolValue)
        {
            byte returnValue = byte.MinValue;

            if (boolValue)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion

        #region ParseByteArray
        /// <summary>
        /// 将 Object 类型转换为 byte[] 型,转换失败时返回 null
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 byte[]</returns>
        public static byte[] ParseByteArray(object objValue)
        {
            byte[] returnValue = null;

            if (objValue != null && objValue != DBNull.Value)
            {
                try
                {
                    returnValue = (byte[])objValue;
                }
                catch
                {
                    returnValue = null;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 string 类型转换为 byte[] 型(使用UTF8编码),转换失败时返回 null
        /// </summary>
        /// <param name="strValue">待转换 string</param>      
        /// <returns>返回 byte[]</returns>
        public static byte[] ParseByteArray(string strValue)
        {
            byte[] returnValue = null;

            if (!string.IsNullOrEmpty(strValue))
            {
                try
                {
                    returnValue = System.Text.Encoding.UTF8.GetBytes(strValue);
                }
                catch
                {
                    returnValue = null;
                }
            }

            return returnValue;
        }
        #endregion

        #region ParseShort (short : -32,768 - 32,767; 2 字节)
        /// <summary>
        /// 将 String 类型转换为 short 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 short</returns>
        public static short ParseShort(string strValue, short defaultValue)
        {
            short returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!short.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 short 型,转换失败返回 short.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 short</returns>
        public static short ParseShort(string strValue)
        {
            short returnValue = short.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!short.TryParse(strValue, out returnValue))
                {
                    returnValue = short.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 short 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 short</returns>
        public static short ParseShort(object objValue, short defaultValue)
        {
            short returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!short.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 short 型,转换失败时返回 short.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 short</returns>
        public static short ParseShort(object objValue)
        {
            short returnValue = short.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!short.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = short.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 bool 类型转换为 short 型 (true--1;false--0)
        /// </summary>
        /// <param name="boolValue">待转换 bool</param>      
        /// <returns>返回 short</returns>
        public static short ParseShort(bool boolValue)
        {
            short returnValue = short.MinValue;

            if (boolValue)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion

        #region ParseInt (int : -2,147,483,648 - 2,147,483,647; 4 字节)
        /// <summary>
        /// 将 String 类型转换为 int 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 int</returns>
        public static int ParseInt(string strValue, int defaultValue)
        {
            int returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!int.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 int 型,转换失败返回 int.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 int</returns>
        public static int ParseInt(string strValue)
        {
            int returnValue = int.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!int.TryParse(strValue, out returnValue))
                {
                    returnValue = int.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 int 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 int</returns>
        public static int ParseInt(object objValue, int defaultValue)
        {
            int returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!int.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 int 型,转换失败时返回 int.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 int</returns>
        public static int ParseInt(object objValue)
        {
            int returnValue = int.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!int.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = int.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 DateTime 类型转换为 int (yyyyMMdd) 型,转换失败返回 -1
        /// </summary>
        /// <param name="dtValue">待转换 DateTime</param>
        /// <returns>返回 int</returns>
        public static int ParseInt(DateTime dtValue)
        {
            int returnValue = -1;

            if (dtValue != null)
            {
                returnValue = dtValue.Year * 10000 + dtValue.Month * 100 + dtValue.Day;
            }

            return returnValue;
        }

        /// <summary>
        /// 将 bool 类型转换为 int 型 (true--1;false--0)
        /// </summary>
        /// <param name="boolValue">待转换 bool</param>      
        /// <returns>返回 int</returns>
        public static int ParseInt(bool boolValue)
        {
            int returnValue = 0;

            if (boolValue)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion

        #region ParseLong (long : -(10 的 19 次方) - +(10 的 19 次方); 8 字节)
        /// <summary>
        /// 将 String 类型转换为 long 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 long</returns>
        public static long ParseLong(string strValue, long defaultValue)
        {
            long returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!long.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 long 型,转换失败返回 long.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 long</returns>
        public static long ParseLong(string strValue)
        {
            long returnValue = long.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!long.TryParse(strValue, out returnValue))
                {
                    returnValue = long.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 long 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 long</returns>
        public static long ParseLong(object objValue, long defaultValue)
        {
            long returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!long.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 long 型,转换失败时返回 long.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 long</returns>
        public static long ParseLong(object objValue)
        {
            long returnValue = long.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!long.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = long.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 DateTime 类型转换为 long 型,转换失败返回 long.MinValue
        /// </summary>
        /// <param name="dtValue">待转换 DateTime</param>
        /// <returns>返回 long</returns>
        public static long ParseLong(DateTime dtValue)
        {
            long returnValue = long.MinValue;

            if (dtValue != null)
            {
                try
                {
                    returnValue = dtValue.Ticks;
                }
                catch { }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 bool 类型转换为 long 型 (true--1;false--0)
        /// </summary>
        /// <param name="boolValue">待转换 bool</param>      
        /// <returns>返回 long</returns>
        public static long ParseLong(bool boolValue)
        {
            long returnValue = long.MinValue;

            if (boolValue)
            {
                returnValue = 1;
            }
            else
            {
                returnValue = 0;
            }

            return returnValue;
        }
        #endregion

        #region ParseFloat (float : 4 字节)
        /// <summary>
        /// 将 String 类型转换为 float 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 float</returns>
        public static float ParseFloat(string strValue, float defaultValue)
        {
            float returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!float.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 float 型,转换失败返回 float.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 float</returns>
        public static float ParseFloat(string strValue)
        {
            float returnValue = float.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!float.TryParse(strValue, out returnValue))
                {
                    returnValue = float.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 float 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 float</returns>
        public static float ParseFloat(object objValue, float defaultValue)
        {
            float returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!float.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 float 型,转换失败时返回 float.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 float</returns>
        public static float ParseFloat(object objValue)
        {
            float returnValue = float.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!float.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = float.MinValue;
                }
            }

            return returnValue;
        }
        #endregion

        #region ParseDouble (double : 8 字节)
        /// <summary>
        /// 将 String 类型转换为 double 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 double</returns>
        public static double ParseDouble(string strValue, double defaultValue)
        {
            double returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!double.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 double 型,转换失败返回 double.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 double</returns>
        public static double ParseDouble(string strValue)
        {
            double returnValue = double.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!double.TryParse(strValue, out returnValue))
                {
                    returnValue = double.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 double 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 double</returns>
        public static double ParseDouble(object objValue, double defaultValue)
        {
            double returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!double.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 double 型,转换失败时返回 double.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 double</returns>
        public static double ParseDouble(object objValue)
        {
            double returnValue = double.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!double.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = double.MinValue;
                }
            }

            return returnValue;
        }
        #endregion

        #region ParseDecimal (Decimal : 16 字节)
        /// <summary>
        /// 将 String 类型转换为 Decimal 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(string strValue, decimal defaultValue)
        {
            decimal returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!decimal.TryParse(strValue, out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 Decimal 型,转换失败返回 decimal.MinValue
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(string strValue)
        {
            decimal returnValue = decimal.MinValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (!decimal.TryParse(strValue, out returnValue))
                {
                    returnValue = decimal.MinValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 Decimal 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(object objValue, decimal defaultValue)
        {
            decimal returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!decimal.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = defaultValue;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 Decimal 型,转换失败时返回 decimal.MinValue
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 Decimal</returns>
        public static decimal ParseDecimal(object objValue)
        {
            decimal returnValue = decimal.MinValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (!decimal.TryParse(objValue.ToString(), out returnValue))
                {
                    returnValue = decimal.MinValue;
                }
            }

            return returnValue;
        }
        #endregion

        #region ParseDateTime
        /// <summary>
        /// 将 String 类型转换为 DateTime 型,转换失败时返回默认值
        /// </summary>
        /// <param name="strValue">待转换 String</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(string strValue, DateTime defaultValue)
        {
            DateTime returnValue = defaultValue;

            if (!string.IsNullOrEmpty(strValue))
            {
                if (strValue.Length == 8) //yyyyMMdd 格式
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(strValue.Substring(0, 4));
                    sb.Append("-");
                    sb.Append(strValue.Substring(4, 2));
                    sb.Append("-");
                    sb.Append(strValue.Substring(6, 2));

                    if (!DateTime.TryParse(sb.ToString(), out returnValue))
                    {
                        returnValue = defaultValue;
                    }
                }
                else //yyyy-MM-dd,yyyy/MM/dd 等格式
                {
                    if (!DateTime.TryParse(strValue, out returnValue))
                    {
                        returnValue = defaultValue;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 String 类型转换为 DateTime 型,转换失败返回 日期1900-1-1
        /// </summary>
        /// <param name="strValue">待转 String</param>
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(string strValue)
        {
            DateTime returnValue = new DateTime(1900, 1, 1);

            if (!string.IsNullOrEmpty(strValue))
            {
                if (strValue.Length == 8) //yyyyMMdd 格式
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(strValue.Substring(0, 4));
                    sb.Append("-");
                    sb.Append(strValue.Substring(4, 2));
                    sb.Append("-");
                    sb.Append(strValue.Substring(6, 2));

                    if (!DateTime.TryParse(sb.ToString(), out returnValue))
                    {
                        returnValue = new DateTime(1900, 1, 1);
                    }
                }
                else //yyyy-MM-dd,yyyy/MM/dd 等格式
                {
                    if (!DateTime.TryParse(strValue, out returnValue))
                    {
                        returnValue = new DateTime(1900, 1, 1);
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 DateTime 型,转换失败时返回默认值
        /// </summary>
        /// <param name="objValue">待转换 Object</param>
        /// <param name="defaultValue">转换失败时的默认值</param>
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(object objValue, DateTime defaultValue)
        {
            DateTime returnValue = defaultValue;

            if (objValue != null && objValue != DBNull.Value)
            {
                if (objValue.ToString().Length == 8) //yyyyMMdd 格式
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(objValue.ToString().Substring(0, 4));
                    sb.Append("-");
                    sb.Append(objValue.ToString().Substring(4, 2));
                    sb.Append("-");
                    sb.Append(objValue.ToString().Substring(6, 2));

                    if (!DateTime.TryParse(sb.ToString(), out returnValue))
                    {
                        returnValue = defaultValue;
                    }
                }
                else //yyyy-MM-dd,yyyy/MM/dd 等格式

                {
                    if (!DateTime.TryParse(objValue.ToString(), out returnValue))
                    {
                        returnValue = defaultValue;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 Object 类型转换为 DateTime 型,转换失败时返回 日期1900-1-1
        /// </summary>
        /// <param name="objValue">待转换 Object</param>      
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(object objValue)
        {
            DateTime returnValue = new DateTime(1900, 1, 1);

            if (objValue != null && objValue != DBNull.Value)
            {
                if (objValue.ToString().Length == 8) //yyyyMMdd 格式
                {
                    StringBuilder sb = new StringBuilder();

                    sb.Append(objValue.ToString().Substring(0, 4));
                    sb.Append("-");
                    sb.Append(objValue.ToString().Substring(4, 2));
                    sb.Append("-");
                    sb.Append(objValue.ToString().Substring(6, 2));

                    if (!DateTime.TryParse(sb.ToString(), out returnValue))
                    {
                        returnValue = new DateTime(1900, 1, 1);
                    }
                }
                else //yyyy-MM-dd,yyyy/MM/dd 等格式

                {
                    if (!DateTime.TryParse(objValue.ToString(), out returnValue))
                    {
                        returnValue = new DateTime(1900, 1, 1);
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 int (yyyyMMdd) 类型转换为 DateTime 型,转换失败时返回 日期1900-1-1
        /// </summary>
        /// <param name="intValue">待转换 int</param>
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(int intValue)
        {
            int year = 0;
            int month = 0;
            int day = 0;

            DateTime returnValue = new DateTime(1900, 1, 1);

            if (intValue >= 19000101 && intValue <= 99991231)
            {
                year = intValue / 10000;
                month = (intValue % year) / 100;
                day = intValue % (year * 100 + month);

                if ((month >= 1 && month <= 12) && (day >= 1 && day <= 31))
                {
                    try
                    {
                        returnValue = new DateTime(year, month, day);
                    }
                    catch { }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 将 long 类型转换为 DateTime 型,转换失败时返回 日期1900-1-1
        /// </summary>
        /// <param name="lngValue">待转换 long</param>
        /// <returns>返回 DateTime</returns>
        public static DateTime ParseDateTime(long lngValue)
        {
            DateTime returnValue = new DateTime(1900, 1, 1);

            try
            {
                returnValue = new DateTime(lngValue);
            }
            catch { }

            return returnValue;
        }
        #endregion

        #region 全角半角转换
        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="strInput">任意字符串</param>
        /// <returns>全角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToSBC(string strInput)
        {
            string strResult = "";

            if (!string.IsNullOrEmpty(strInput))
            {
                char[] arrChar = strInput.ToCharArray();

                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (arrChar[i] == 32)
                    {
                        arrChar[i] = (char)12288;
                        continue;
                    }

                    if (arrChar[i] < 127)
                    {
                        arrChar[i] = (char)(arrChar[i] + 65248);
                    }
                }

                strResult = new string(arrChar);
            }

            return strResult;
        }


        /// <summary> 转半角的函数(DBC case) </summary>
        /// <param name="strInput">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string strInput)
        {
            string strResult = "";

            if (!string.IsNullOrEmpty(strInput))
            {
                char[] arrChar = strInput.ToCharArray();

                for (int i = 0; i < arrChar.Length; i++)
                {
                    if (arrChar[i] == 12288)
                    {
                        arrChar[i] = (char)32;
                        continue;
                    }

                    if (arrChar[i] > 65280 && arrChar[i] < 65375)
                    {
                        arrChar[i] = (char)(arrChar[i] - 65248);
                    }
                }

                strResult = new string(arrChar);
            }

            return strResult;
        }
        #endregion
    }
}
