using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WebConsole.Models.Common.HttpHelper
{
    /// <summary>
    /// 调用方法
    /// HttpsGet httpget = new HttpsGet();
    /// httpget.GetUrlResponse("http://data.gtimg.cn/flashdata/hushen/minute/sz000004.js?maxage=111");
    /// </summary>
    public class HttpsGet
    {
        /// <summary>
        /// 访问安全的https网站时要SSL验证，用回调安全验证的方法，解决这个问题
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public StringBuilder GetUrlResponse(string url)
        {
            HttpWebResponse resp = null;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            resp = (HttpWebResponse)req.GetResponse();

            Stream responseStream = resp.GetResponseStream();
            // 对接响应流(以"GBK"字符集)
            StreamReader sReader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));

            Char[] sReaderBuffer = new Char[256];
            int count = sReader.Read(sReaderBuffer, 0, 256);
            StringBuilder content = new StringBuilder();
            while (count > 0)
            {
                String tempStr = new String(sReaderBuffer, 0, count);
                content.Append(tempStr);
                count = sReader.Read(sReaderBuffer, 0, 256);
            }
            // 读取结束
            sReader.Close();

            return content;
        }
        //处理安全连接问题
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //Always accept
        }
    }
}
