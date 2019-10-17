using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

/******************************************************************
 * Author: miaoxin 
 * Date: 2017-12-26
 * Content: HTTP请求类(基于HttpWebRequest)
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// HTTP请求类(基于HttpWebRequest)
    /// Framework环境中请设置 ServicePointManager.DefaultConnectionLimit = 200;
    /// </summary>
    public class HttpUtil
    {
        #region HttpGet请求(同步，短连接)
        /// <summary>
        /// HttpGet请求(同步，短连接)
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="strContentType">ContentType(默认：text/html;charset=UTF-8)</param>
        /// <param name="strAccept">Accept(默认：application/json,text/javascript,*/*;q=0.01)</param>
        /// <param name="strUserAgent">UserAgent(默认：Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36)</param>
        /// <param name="cookieCollection">CookieCollection</param>
        /// <param name="intTimeOut">TimeOut(默认:100秒)</param>
        /// <param name="strEncoding">Encoding(默认:utf-8)</param>
        /// <returns></returns>
        public static string HttpGet(string strUrl, string strContentType, string strAccept, string strUserAgent, CookieCollection cookieCollection, int intTimeOut, string strEncoding)
        {
            //响应字符串
            string strResult = string.Empty;
            if (string.IsNullOrEmpty(strUrl))
            {
                return strResult;
            }
            if (string.IsNullOrEmpty(strEncoding))
            {
                //默认响应编码为 utf-8
                strEncoding = "utf-8";
            }

            HttpWebRequest request = null;

            try
            {
                request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                return strResult;
            }

            if (request != null)
            {
                //GET请求
                request.Method = "GET";
                //短连接
                request.KeepAlive = false;

                if (string.IsNullOrEmpty(strContentType))
                {
                    request.ContentType = @"text/html;charset=UTF-8";
                }
                else
                {
                    request.ContentType = strContentType;
                }

                if (string.IsNullOrEmpty(strAccept))
                {
                    //request.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Accept = @"application/json,text/javascript,*/*;q=0.01";
                }
                else
                {
                    request.Accept = strAccept;
                }

                if (string.IsNullOrEmpty(strUserAgent))
                {
                    request.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                }
                else
                {
                    request.UserAgent = strUserAgent;
                }

                if (cookieCollection != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookieCollection);
                }

                //超时时间，单位：毫秒（最小10秒）
                if (intTimeOut < 10)
                {
                    request.Timeout = 10 * 1000;
                    request.ReadWriteTimeout = 10 * 1000;
                }
                else
                {
                    request.Timeout = intTimeOut * 1000;
                    request.ReadWriteTimeout = intTimeOut * 1000;
                }

                //AcceptEncoding
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                //代理方式
                //WebProxy proxy = new WebProxy("127.0.0.1", 8000);//指定的ip和端口
                //request.Proxy = proxy;
                //request.AllowAutoRedirect = true;

                //取得响应数据
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                    request.Abort();
                    return strResult;
                }

                if (response != null)
                {
                    byte[] buffer = new byte[4096];
                    int size = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(response.ContentEncoding))
                        {
                            if (response.ContentEncoding.ToLower().Contains("gzip"))
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (GZipStream stream = new GZipStream(rpsStream, CompressionMode.Decompress))
                                        {
                                            using (MemoryStream memoryStream = new MemoryStream())
                                            {
                                                while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                                                {
                                                    memoryStream.Write(buffer, 0, size);
                                                }
                                                strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                            }
                                        }
                                    }
                                }
                            }
                            else if (response.ContentEncoding.ToLower().Contains("deflate"))
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (DeflateStream stream = new DeflateStream(rpsStream, CompressionMode.Decompress))
                                        {
                                            using (MemoryStream memoryStream = new MemoryStream())
                                            {
                                                while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                                                {
                                                    memoryStream.Write(buffer, 0, size);
                                                }
                                                strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (MemoryStream memoryStream = new MemoryStream())
                                        {
                                            while ((size = rpsStream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                memoryStream.Write(buffer, 0, size);
                                            }
                                            strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (Stream rpsStream = response.GetResponseStream())
                            {
                                if (rpsStream != null)
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        while ((size = rpsStream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            memoryStream.Write(buffer, 0, size);
                                        }
                                        strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                        request.Abort();
                        return string.Empty;
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Close();
                        }
                        if (request != null)
                        {
                            request.Abort();
                        }
                    }
                }
            }
            return strResult;
        }

        /// <summary>
        /// HttpGet(Accept：application/json,text/javascript,*/*;q=0.01|TimeOut:100秒|Encoding:utf-8))
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <returns></returns>
        public static string HttpGet(string strUrl)
        {
            return HttpGet(strUrl, string.Empty, string.Empty, string.Empty, null, 100, string.Empty);
        }

        /// <summary>
        /// HttpGet(Accept：application/json,text/javascript,*/*;q=0.01|Encoding:utf-8))
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="intTimeOut">TimeOut</param>
        /// <returns></returns>
        public static string HttpGet(string strUrl, int intTimeOut)
        {
            return HttpGet(strUrl, string.Empty, string.Empty, string.Empty, null, intTimeOut, string.Empty);
        }
        #endregion

        #region HttpPost请求(同步，短连接)
        /// <summary>
        /// HttpPost请求(同步，短连接)
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="strPostData">Post数据</param>
        /// <param name="strContentType">ContentType(默认：application/x-www-form-urlencoded)</param>
        /// <param name="strUserAgent">UserAgent(默认：Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36)</param>
        /// <param name="strAccept">Accept(默认：application/json,text/javascript,*/*;q=0.01)</param>
        /// <param name="cookieCollection">CookieCollection</param>
        /// <param name="intTimeOut">TimeOut(默认:30秒)</param>
        /// <param name="strEncoding">Encoding(默认:utf-8)</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, string strPostData, string strContentType, string strUserAgent, string strAccept, CookieCollection cookieCollection, int intTimeOut, string strEncoding)
        {
            //响应字符串
            string strResult = string.Empty;
            if (string.IsNullOrEmpty(strUrl))
            {
                return strResult;
            }
            if (string.IsNullOrEmpty(strEncoding))
            {
                //默认响应编码为 utf-8
                strEncoding = "utf-8";
            }

            HttpWebRequest request = null;

            try
            {
                if (strUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(strUrl) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                return strResult;
            }

            if (request != null)
            {
                //POST请求
                request.Method = "POST";
                request.KeepAlive = false;
                //request.ServicePoint.Expect100Continue = false;

                if (string.IsNullOrEmpty(strContentType))
                {
                    request.ContentType = @"application/x-www-form-urlencoded";
                }
                else
                {
                    request.ContentType = strContentType;
                }

                if (string.IsNullOrEmpty(strUserAgent))
                {
                    request.UserAgent = @"Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.94 Safari/537.36";
                }
                else
                {
                    request.UserAgent = strUserAgent;
                }

                if (string.IsNullOrEmpty(strAccept))
                {
                    //Requester.Accept = @"text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                    request.Accept = @"application/json,text/javascript,*/*;q=0.01";
                }
                else
                {
                    request.Accept = strAccept;
                }

                if (cookieCollection != null)
                {
                    request.CookieContainer = new CookieContainer();
                    request.CookieContainer.Add(cookieCollection);
                }

                //超时时间，单位：毫秒（默认30秒）
                if (intTimeOut < 30)
                {
                    request.Timeout = 30 * 1000;
                    request.ReadWriteTimeout = 30 * 1000;
                }
                else
                {
                    request.Timeout = intTimeOut * 1000;
                    request.ReadWriteTimeout = intTimeOut * 1000;
                }

                //AcceptEncoding
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

                //代理方式
                //WebProxy proxy = new WebProxy("127.0.0.1", 8000);//指定的ip和端口
                //request.Proxy = proxy;
                //request.AllowAutoRedirect = true;

                //Post数据
                if (!string.IsNullOrEmpty(strPostData))
                {
                    //参数编码
                    //byte[] data = Encoding.UTF8.GetBytes(buffer.ToString());
                    byte[] data = Encoding.GetEncoding(strEncoding).GetBytes(strPostData);

                    using (Stream writeStream = request.GetRequestStream())
                    {
                        if (writeStream != null)
                        {
                            writeStream.Write(data, 0, data.Length);
                        }
                    }
                }

                //取得响应数据
                HttpWebResponse response = null;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception ex)
                {
                    Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                    request.Abort();
                    return strResult;
                }

                if (response != null)
                {
                    byte[] buffer = new byte[4096];
                    int size = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(response.ContentEncoding))
                        {
                            if (response.ContentEncoding.ToLower().Contains("gzip"))
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (GZipStream stream = new GZipStream(rpsStream, CompressionMode.Decompress))
                                        {
                                            using (MemoryStream memoryStream = new MemoryStream())
                                            {
                                                while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                                                {
                                                    memoryStream.Write(buffer, 0, size);
                                                }
                                                strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                            }
                                        }
                                    }
                                }
                            }
                            else if (response.ContentEncoding.ToLower().Contains("deflate"))
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (DeflateStream stream = new DeflateStream(rpsStream, CompressionMode.Decompress))
                                        {
                                            using (MemoryStream memoryStream = new MemoryStream())
                                            {
                                                while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                                                {
                                                    memoryStream.Write(buffer, 0, size);
                                                }
                                                strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                using (Stream rpsStream = response.GetResponseStream())
                                {
                                    if (rpsStream != null)
                                    {
                                        using (MemoryStream memoryStream = new MemoryStream())
                                        {
                                            while ((size = rpsStream.Read(buffer, 0, buffer.Length)) > 0)
                                            {
                                                memoryStream.Write(buffer, 0, size);
                                            }
                                            strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (Stream rpsStream = response.GetResponseStream())
                            {
                                if (rpsStream != null)
                                {
                                    using (MemoryStream memoryStream = new MemoryStream())
                                    {
                                        while ((size = rpsStream.Read(buffer, 0, buffer.Length)) > 0)
                                        {
                                            memoryStream.Write(buffer, 0, size);
                                        }
                                        strResult = Encoding.GetEncoding(strEncoding).GetString(memoryStream.ToArray());
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Log4NetUtil.Error(typeof(HttpUtil), ex.ToString());
                        request.Abort();
                        return string.Empty;
                    }
                    finally
                    {
                        if (response != null)
                        {
                            response.Close();
                        }
                        if (request != null)
                        {
                            request.Abort();
                        }
                    }
                }
            }
            return strResult;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        /// <summary>
        /// HttpPost请求（ContentType：application/x-www-form-urlencoded|Accept：application/json,text/javascript,*/*;q=0.01|TimeOut:60秒|Encoding:utf-8）
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="strPostData">Post数据</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, string strPostData)
        {
            return HttpPost(strUrl, strPostData, string.Empty, string.Empty, string.Empty, null, 60, string.Empty);
        }

        /// <summary>
        /// HttpPost请求（ContentType：application/x-www-form-urlencoded|Accept：application/json,text/javascript,*/*;q=0.01|TimeOut:60秒|Encoding:utf-8）
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="dicPara">Post数据</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, IDictionary<string, string> dicPara)
        {
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in dicPara.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, dicPara[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, dicPara[key]);
                }
                i++;
            }

            return HttpPost(strUrl, buffer.ToString(), string.Empty, string.Empty, string.Empty, null, 60, string.Empty);
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="strPostData">Post数据</param>
        /// <param name="intTimeOut">TimeOut(默认:30秒)</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, string strPostData, int intTimeOut)
        {
            return HttpPost(strUrl, strPostData, string.Empty, string.Empty, string.Empty, null, intTimeOut, string.Empty);
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="dicPara">Post数据</param>
        /// <param name="intTimeOut">TimeOut(默认:30秒)</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, IDictionary<string, string> dicPara, int intTimeOut)
        {
            StringBuilder buffer = new StringBuilder();
            int i = 0;
            foreach (string key in dicPara.Keys)
            {
                if (i > 0)
                {
                    buffer.AppendFormat("&{0}={1}", key, dicPara[key]);
                }
                else
                {
                    buffer.AppendFormat("{0}={1}", key, dicPara[key]);
                }
                i++;
            }

            return HttpPost(strUrl, buffer.ToString(), string.Empty, string.Empty, string.Empty, null, intTimeOut, string.Empty);
        }

        /// <summary>
        /// HttpPost请求
        /// </summary>
        /// <param name="strUrl">Url</param>
        /// <param name="strPostData">Post数据</param>
        /// <param name="strContentType">ContentType(默认：application/x-www-form-urlencoded)</param>
        /// <param name="intTimeOut">TimeOut(默认:30秒)</param>
        /// <returns></returns>
        public static string HttpPost(string strUrl, string strPostData, string strContentType, int intTimeOut)
        {
            return HttpPost(strUrl, strPostData, strContentType, string.Empty, string.Empty, null, intTimeOut, string.Empty);
        }
        #endregion
    }
}
