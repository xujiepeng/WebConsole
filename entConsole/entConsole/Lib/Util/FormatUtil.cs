using System;
using System.Text;
using System.Text.RegularExpressions;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-16
 * Content: 数据格式化类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 数据格式化类
    /// </summary>
    public class FormatUtil
    {
        #region 格式化 Byte 类型
        /// <summary>
        /// 格式化 Byte 类型
        /// 
        ///  例： 234
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥234.00         C4: ￥234.0000 )
        ///  (D) 十进制数   Decimal . . . . . . . . . ( D: 234              D2:234            D6: 000234 )
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: 2.340000E+002    E2: 2.34E+002     E3:2.340E+002)
        ///  (F) 定点       Fixed point . . . . . . . ( F: 234.00           F4: 234.0000 )
        ///  (G) 常规       General . . . . . . . . . ( G: 234              G2: 2.3E+02      G6: 234)
        ///  (N) 数字       Number  . . . . . . . . . ( N: 234.00           N4: 234.0000)
        ///  (P) 百分比     Percent . . . . . . . . . ( P: 23,400.00%       P4: 23,400.0000%)       
        ///  (X) 十六进制数 Hexadecimal . . . . . . . ( X:EA                X10:00000000EA)
        /// </summary>
        /// <param name="byteValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(byte byteValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return byteValue.ToString();
            }

            return byteValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Short 类型
        /// <summary>
        /// 格式化 Short 类型
        /// 
        ///  例： -2345
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.00      C4: ￥-2,345.0000 )
        ///  (D) 十进制数   Decimal . . . . . . . . . ( D: -2345            D2:-2345          D6: -002345 )
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345000E+003   E2: -2.35E+003    E3:-2.345E+003)
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.00         F4: -2345.0000 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345            G2: -2.3E+03      G6: -2345)
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.00        N4: -2,345.0000)
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,500.00%     P4: -234,500.0000%)       
        ///  (X) 十六进制数 Hexadecimal . . . . . . . ( X:F6D7              X10:000000F6D7)
        /// </summary>
        /// <param name="shortValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(short shortValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return shortValue.ToString();
            }

            return shortValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Int 类型
        /// <summary>
        /// 格式化 Int 类型
        /// 
        ///  例： -2345
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.00      C4: ￥-2,345.0000 )
        ///  (D) 十进制数   Decimal . . . . . . . . . ( D: -2345            D2:-2345          D6: -002345 )
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345000E+003   E2: -2.35E+003    E3:-2.345E+003)
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.00         F4: -2345.0000 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345            G2: -2.3E+03      G6: -2345)
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.00        N4: -2,345.0000)
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,500.00%     P4: -234,500.0000%)     
        ///  (X) 十六进制数 Hexadecimal . . . . . . . ( X: FFFFF6D7         X10: 00FFFFF6D7)
        /// </summary>
        /// <param name="intValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(int intValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return intValue.ToString();
            }

            return intValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Long 类型
        /// <summary>
        /// 格式化 Long 类型
        /// 
        ///  例： -2345
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.00      C4: ￥-2,345.0000 )
        ///  (D) 十进制数   Decimal . . . . . . . . . ( D: -2345            D2:-2345          D6: -002345 )
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345000E+003   E2: -2.35E+003    E3:-2.345E+003)
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.00         F4: -2345.0000 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345            G2: -2.3E+03      G6: -2345)
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.00        N4: -2,345.0000)
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,500.00%     P4: -234,500.0000%)     
        ///  (X) 十六进制数 Hexadecimal . . . . . . . ( X: FFFFFFFFFFFFF6D7 X10: FFFFFFFFFFFFF6D7)
        /// </summary>
        /// <param name="lngValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(long lngValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return lngValue.ToString();
            }

            return lngValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Float 类型
        /// <summary>
        /// 格式化 Float 类型
        /// 
        /// 注意：float有效位数只有7位
        /// 
        ///  例： -2345.1234
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.12      C4: ￥-2,345.1230 )        
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345123E+003   E2: -2.35E+003    E3:-2.345E+003 )
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.12         F4: -2345.1230 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345.123        G2: -2.3E+03      G6: -2345.12 )
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.12        N4: -2,345.1230 )
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,512.30%     P4: -234,512.3000% )     
        /// </summary>
        /// <param name="fltValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(float fltValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return fltValue.ToString();
            }

            return fltValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Double 类型
        /// <summary>
        /// 格式化 Double 类型
        /// 
        /// 注意：Double有效位数有15/16位
        /// 
        ///  例： -2345.1234
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.12      C4: ￥-2,345.1234  G6: ￥-2,345.123400 )        
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345123E+003   E2: -2.35E+003    E3: -2.345E+003 )
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.12         F4: -2345.1234 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345.1234       G2: -2.3E+03      G6: -2345.12 )
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.12        N4: -2,345.1234 )
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,512.34%     P4: -234,512.3400% )    
        /// </summary>
        /// <param name="dblValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(double dblValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return dblValue.ToString();
            }

            Regex reg = new Regex(@"^(C|c|E|e|F|f|G|g|N|n|P|p)[0-9]{0,2}$");

            if (!reg.IsMatch(strFormatType))
            {
                return dblValue.ToString();
            }

            return dblValue.ToString(strFormatType);
        }
        #endregion

        #region 格式化 Decimal 类型
        /// <summary>
        /// 格式化 Decimal 类型
        /// 
        /// 注意：Decimal有效位数有X位
        /// 
        ///  例： -2345.1234
        ///  (C) 货币       Currency  . . . . . . . . ( C: ￥-2,345.12      C4: ￥-2,345.1230 )        
        ///  (E) 科学记数法 Scientific  . . . . . . . ( E: -2.345123E+003   E2: -2.35E+003    E3:-2.345E+003 )
        ///  (F) 定点       Fixed point . . . . . . . ( F: -2345.12         F4: -2345.1230 )
        ///  (G) 常规       General . . . . . . . . . ( G: -2345.123        G2: -2.3E+03      G6: -2345.12 )
        ///  (N) 数字       Number  . . . . . . . . . ( N: -2,345.12        N4: -2,345.1230 )
        ///  (P) 百分比     Percent . . . . . . . . . ( P: -234,512.30%     P4: -234,512.3000% )     
        /// </summary>
        /// <param name="decValue">格式化数据</param>
        /// <param name="strFormatType">格式化参数</param>
        /// <returns>返回结果</returns>
        public static string FormatNumber(decimal decValue, string strFormatType)
        {
            if (string.IsNullOrEmpty(strFormatType))
            {
                return decValue.ToString();
            }

            return decValue.ToString(strFormatType);
        }
        #endregion

        #region 截断指定长度字符串
        /// <summary>
        /// 截断指定长度字符串
        /// </summary>
        /// <param name="strPara">原字符串</param>
        /// <param name="intLength">限制长度</param>
        /// <param name="strEndWith">结束字符串</param>
        /// <returns>截断后的字符串</returns>
        public static string SubStr(string strPara, int intLength, string strEndWith)
        {
            StringBuilder sb = new StringBuilder();
            int intTotalByteNum = 0;

            if (string.IsNullOrEmpty(strPara))
            {
                return "";
            }

            if (string.IsNullOrEmpty(strEndWith))
            {
                strEndWith = "...";
            }

            for (int i = 0; i < strPara.Length; i++)
            {
                intTotalByteNum += CheckUtil.GetCharacterByte(strPara[i].ToString());

                if (intTotalByteNum > intLength)
                {
                    break;
                }

                sb.Append(strPara[i]);
            }

            if (sb.ToString() != strPara)
            {
                sb.Append(strEndWith);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 截断指定长度字符串(默认以"..."结尾)
        /// </summary>
        /// <param name="strPara">原字符串</param>
        /// <param name="intLength">限制长度</param>
        /// <returns>截断后的字符串</returns>
        public static string SubStr(string strPara, int intLength)
        {
            return SubStr(strPara, intLength, "...");
        }
        #endregion

        #region 格式化日期
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="strDateTime"></param>
        /// <param name="strFormatString"></param>
        /// <returns></returns>
        public static string FormatDateTime(string strDateTime, string strFormatString)
        {
            DateTime dt;

            if (string.IsNullOrEmpty(strDateTime))
            {
                return "";
            }

            if (string.IsNullOrEmpty(strFormatString))
            {
                return strDateTime;
            }

            if (!DateTime.TryParse(strDateTime, out dt))
            {
                dt = new DateTime(1900, 1, 1);
            }

            return dt.ToString(strFormatString);
        }
        #endregion

        #region Base64编码
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="strInput">strInput</param>
        /// <returns></returns>
        public static string StrToBase64(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }
            byte[] inputArry = Encoding.UTF8.GetBytes(strInput);

            return Convert.ToBase64String(inputArry);
        }
        #endregion

        #region Base64解码
        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="strInput">strInput</param>
        /// <returns></returns>
        public static string Base64ToStr(string strInput)
        {
            if (string.IsNullOrEmpty(strInput))
            {
                return "";
            }
            byte[] inputArry = Convert.FromBase64String(strInput);

            return Encoding.UTF8.GetString(inputArry);
        }
        #endregion
    }
}
