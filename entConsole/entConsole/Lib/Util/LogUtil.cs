using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-19
 * Content: 异步文件日志类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 日志级别枚举
    /// </summary>
    public enum LogLevel
    {
        Trace = 1,  //跟踪信息
        Debug = 2,  //调试信息
        Warn  = 3,  //警告信息
        Error = 4,  //一般错误
        Fatal = 5,  //致命错误
        Off   = 6   //关闭
    }

    /// <summary>
    /// 异步文件日志类
    /// </summary>
    public class LogUtil
    {
        //互斥令牌对象
        private static object objLock = new object();

        //输出日志级别
        private static LogLevel enumOutLogLevel = LogLevel.Off;

        //日志扩展名
        private static string strExtension = ".log";

        //日志基路径
        private static string strBaseLogPath = string.Empty;

        //最大日志容量(Kb)
        private static long MaxLogSize = 2048;

        #region 初始化日志设置
        /// <summary>
        /// 初始化日志设置
        /// </summary>
        /// <param name="OutLogLevel">输出日志级别(Trace,Debug,Warn,Error,Fatal,Off)</param>
        /// <param name="BaseLogPath">日志基路径(绝对路径)</param>
        /// <param name="lngMaxLogSize">最大日志容量(Kb)，默认2048Kb</param>
        public static void InitLogSetting(string OutLogLevel, string BaseLogPath, long lngMaxLogSize = 2048)
        {
            if (string.IsNullOrEmpty(BaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.InitLogSetting->BaseLogPath is Empty");
            }
            if (!Directory.Exists(BaseLogPath))
            {
                Directory.CreateDirectory(BaseLogPath);
            }
            strBaseLogPath = BaseLogPath;

            switch (OutLogLevel.ToLower().Trim())
            {
                case "trace":
                    enumOutLogLevel = LogLevel.Trace;
                    break;
                case "debug":
                    enumOutLogLevel = LogLevel.Debug;
                    break;
                case "warn":
                    enumOutLogLevel = LogLevel.Warn;
                    break;
                case "error":
                    enumOutLogLevel = LogLevel.Error;
                    break;
                case "fatal":
                    enumOutLogLevel = LogLevel.Fatal;
                    break;
                case "off":
                    enumOutLogLevel = LogLevel.Off;
                    break;
                default:
                    enumOutLogLevel = LogLevel.Off;
                    break;
            }

            MaxLogSize = lngMaxLogSize;
        }
        #endregion

        #region Trace
        /// <summary>
        /// 跟踪信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Trace(string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Trace->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Trace))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Trace, "", "", "", strMsg));
        }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Trace(string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Trace->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Trace))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Trace, strClassName, strMethodName, "", strMsg));
        }

        /// <summary>
        /// 跟踪信息
        /// </summary>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Trace(string strServiceName, string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Trace->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Trace))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Trace, strClassName, strMethodName, strServiceName, strMsg));
        }
        #endregion

        #region Debug
        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Debug(string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Debug->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Debug))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Debug, "", "", "", strMsg));
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Debug(string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Debug->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Debug))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Debug, strClassName, strMethodName, "", strMsg));
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Debug(string strServiceName, string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Debug->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Debug))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Debug, strClassName, strMethodName, strServiceName, strMsg));
        }
        #endregion

        #region Warn
        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Warn(string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Warn->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Warn))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Warn, "", "", "", strMsg));
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Warn(string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Warn->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Warn))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Warn, strClassName, strMethodName, "", strMsg));
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Warn(string strServiceName, string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Warn->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Warn))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Warn, strClassName, strMethodName, strServiceName, strMsg));
        }
        #endregion

        #region Error
        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Error(string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Error->BaseLogPath is Empty");
            }
            
            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Error))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Error, "", "", "", strMsg));
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Error(string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Error->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Error))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Error, strClassName, strMethodName, "", strMsg));
        }

        /// <summary>
        /// 一般错误
        /// </summary>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Error(string strServiceName, string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Error->BaseLogPath is Empty");
            }
            
            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Error))
            {
                return;
            }
            Task t = Task.Factory.StartNew(()=> WriteLog(LogLevel.Error, strClassName, strMethodName, strServiceName, strMsg));
        }
        #endregion

        #region Fatal
        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="strMsg">日志信息</param>
        public static void Fatal(string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Fatal->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Fatal))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Fatal, "", "", "", strMsg));
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Fatal(string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Fatal->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Fatal))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Fatal, strClassName, strMethodName, "", strMsg));
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strMsg">日志信息</param>
        public static void Fatal(string strServiceName, string strClassName, string strMethodName, string strMsg)
        {
            //日志基目录检查
            if (string.IsNullOrEmpty(strBaseLogPath))
            {
                throw new ArgumentException("AnsyFileLogUtil.Fatal->BaseLogPath is Empty");
            }

            //日志级别检查（只输出高于配置级别的日志）
            if (!CheckLevel(LogLevel.Fatal))
            {
                return;
            }
            Task t = Task.Factory.StartNew(() => WriteLog(LogLevel.Fatal, strClassName, strMethodName, strServiceName, strMsg));
        }
        #endregion

        #region 日志级别检查（只输出高于配置级别的日志）
        /// <summary>
        /// 日志级别检查（只输出高于配置级别的日志）
        /// </summary>
        /// <param name="enumLogLevel">待检查的日志级别</param>
        /// <returns>符合输出返回true,否则返回false</returns>
        private static bool CheckLevel(LogLevel enumLogLevel)
        {
            if (enumOutLogLevel == LogLevel.Off)
            {
                return false;
            }

            if (enumLogLevel >= enumOutLogLevel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 写日志
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="enumLogLevel">日志级别</param>
        /// <param name="strClassName">类名</param>
        /// <param name="strMethodName">方法名</param>
        /// <param name="strServiceName">服务名</param>
        /// <param name="strMsg">日志信息</param>
        private static void WriteLog(LogLevel enumLogLevel, string strClassName, string strMethodName, string strServiceName, string strMsg)
        {
            if (string.IsNullOrEmpty(strMsg))
            {
                return;
            }
            
            lock (objLock)
            {
                //日志路径
                string str_LogPath = strBaseLogPath;
                //日志文件
                string strLogFile = string.Empty;

                //根据不同的服务区分目录
                if (!string.IsNullOrEmpty(strServiceName))
                {
                    str_LogPath = Path.Combine(str_LogPath, strServiceName);
                }

                //日志路径格式 yyyy-mm
                str_LogPath = Path.Combine(str_LogPath, DateTime.Now.ToString("yyyy-MM"));

                //日志全名
                strLogFile = Path.Combine(str_LogPath, DateTime.Now.ToString("yyyy-MM-dd") + strExtension);

                //写入流
                StreamWriter sw = null;

                try
                {
                    //如果路径不存在，重新创建
                    if (!Directory.Exists(str_LogPath))
                    {
                        Directory.CreateDirectory(str_LogPath);
                    }

                    if (File.Exists(strLogFile))
                    {
                        if (GetFileSize(strLogFile) >= MaxLogSize)
                        {
                            //新日志文件名称
                            string strNewLogFile = GetNewFileName(str_LogPath);

                            if (string.IsNullOrEmpty(strNewLogFile))
                            {
                                throw new Exception("NewLogFile is Empty");
                            }
                            else
                            {
                                if (File.Exists(strNewLogFile))
                                {
                                    sw = File.AppendText(strNewLogFile);
                                }
                                else
                                {
                                    sw = File.CreateText(strNewLogFile);
                                }
                            }
                        }
                        else
                        {
                            sw = File.AppendText(strLogFile);
                        }
                    }
                    else
                    {
                        sw = File.CreateText(strLogFile);
                    }

                    //写日志
                    sw.WriteLine("**********************************************************************************************");
                    sw.WriteLine("Log Time     : " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    sw.WriteLine("Log Level    : " + Enum.GetName(typeof(LogLevel), enumLogLevel));

                    if (!string.IsNullOrEmpty(strServiceName))
                    {
                        sw.WriteLine("Service Name : " + strServiceName);
                    }

                    sw.WriteLine("Class Name   : " + strClassName);
                    sw.WriteLine("Method Name  : " + strMethodName);
                    sw.WriteLine("Log Msg      : " + strMsg);
                    sw.Flush();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (sw != null)
                    {
                        try
                        {
                            sw.Close();
                            sw.Dispose();
                        }
                        catch { }
                    }
                }
            }
        }
        #endregion

        #region 取得新文件名称
        /// <summary>
        /// 取得新文件名称
        /// </summary>
        /// <param name="strFilePath">目录</param>
        /// <returns></returns>
        private static string GetNewFileName(string strFilePath)
        {
            if (string.IsNullOrEmpty(strFilePath) || !Directory.Exists(strFilePath))
            {
                return string.Empty;
            }

            //新文件名称
            string strNewName = string.Empty;
            //临时变量
            string tempName = string.Empty;

            //取得文件列表（以yyyy-MM-dd开头 .log结尾）
            List<string> list = GetDirFileList(strFilePath, DateTime.Now.ToString("yyyy-MM-dd") + "*"+ strExtension);
            if (list == null)
            {
                return string.Empty;
            }

            //筛选 YYYY-MM-DD_d 和 YYYY-MM-DD 格式 并对文件名排序（按序号升序）
            list = list.Where((a) => {
                tempName = Path.GetFileNameWithoutExtension(a);
                return (CheckLogDate_d(tempName) || CheckLogDate(tempName));
            }).OrderBy((o)=> {
                tempName = Path.GetFileNameWithoutExtension(o);
                if (CheckLogDate(tempName))  //YYYY-MM-DD
                {
                    return 0;
                }
                else if (CheckLogDate_d(tempName))  //YYYY-MM-DD_d
                {
                    return ConvertUtil.ParseInt(tempName.Split('_', StringSplitOptions.RemoveEmptyEntries)[1]);
                }
                else
                {
                    return 0;
                }
            }).ToList();

            for (int i = list.Count - 1; i >= 0; i--)
            {
                tempName = Path.GetFileNameWithoutExtension(list[i]);

                if (CheckLogDate_d(tempName))  //YYYY-MM-DD_d
                {
                    //判断最后一个文件的大小
                    if (GetFileSize(list[i]) >= MaxLogSize)
                    {
                        int intNo = ConvertUtil.ParseInt(tempName.Split('_', StringSplitOptions.RemoveEmptyEntries)[1]);
                        intNo++;
                        strNewName = Path.Combine(strFilePath, DateTime.Now.ToString("yyyy-MM-dd") + "_" + intNo.ToString() + strExtension);
                    }
                    else
                    {
                        strNewName = list[i];
                    }
                    break;
                }
                else if (CheckLogDate(tempName))  //YYYY-MM-DD
                {                    
                    strNewName = Path.Combine(strFilePath, tempName + "_1" + strExtension);
                    break;
                }
                else  //不合法日志名称
                {                    
                    continue;
                }
            }
            
            return strNewName;
        }
        #endregion

        #region 取得文件大小（单位:Kb）
        /// <summary>
        /// 取得文件大小（单位:Kb）
        /// </summary>
        /// <param name="strFilePath">文件路径</param>
        /// <returns>Kb</returns>
        private static long GetFileSize(string strFilePath)
        {
            if (string.IsNullOrEmpty(strFilePath) || !File.Exists(strFilePath))
            {
                return long.MinValue;
            }

            FileInfo fi = new FileInfo(strFilePath);
            return fi.Length / 1024;
        }
        #endregion

        #region 取得指定目录文件列表
        /// <summary>
        /// 取得指定目录文件列表
        /// </summary>
        /// <param name="strFilePath">目录</param>
        /// <param name="strPattern">筛选条件</param>
        /// <returns></returns>
        private static List<string> GetDirFileList(string strFilePath,string strPattern="*.*")
        {
            if (string.IsNullOrEmpty(strFilePath) || !Directory.Exists(strFilePath))
            {
                return null;
            }

            List<string> list = Directory.GetFiles(strFilePath, strPattern).ToList<string>();
            return list;
        }
        #endregion

        #region 正则表达式匹配 日志日期 格式字符串 (YYYY-MM-DD)
        /// <summary>
        /// 正则表达式匹配 日志日期 格式字符串 (YYYY-MM-DD)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        private static bool CheckLogDate(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9]$");

            return reg.IsMatch(strPara);
        }
        #endregion

        #region 正则表达式匹配 日志日期 格式字符串 (YYYY-MM-DD_d)
        /// <summary>
        /// 正则表达式匹配 日志日期 格式字符串 (YYYY-MM-DD_d)
        /// </summary>
        /// <param name="strPara"></param>
        /// <returns></returns>
        private static bool CheckLogDate_d(string strPara)
        {
            if (string.IsNullOrEmpty(strPara))
            {
                return false;
            }

            Regex reg = new Regex(@"^[1-9][0-9]{3}-[0|1][0-9]-[0-3][0-9]_\d+$");

            return reg.IsMatch(strPara);
        }
        #endregion
    }
}
