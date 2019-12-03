using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models.Model;
using System.Net;
using System.Text;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace WebConsole.Controllers
{
    public class EventController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            string str = getUrlResponse("http://data.gtimg.cn/flashdata/hushen/minute/sz000004.js?maxage=111").ToString();
            string[] array0 = str.Split("\\n\\\n");
            string[] arraynew = new string[array0.Length - 1];
            for (int i = 2; i < array0.Length-2; i++)
            {
                arraynew[i-2] = array0[i].Split(' ')[1];
            }
            return View();
        }

        public string DrawLine(string id)
        {
            string str = getUrlResponse("http://data.gtimg.cn/flashdata/hushen/minute/sz000004.js?maxage=111").ToString();
            string[] array0 = str.Split("\\n\\\n");
            string[] arraynew = new string[array0.Length - 3];
            for (int i = 2; i < array0.Length - 1; i++)
            {
                arraynew[i - 2] = array0[i].Split(' ')[1];
            }
            double[] iNumsnew = Array.ConvertAll<string, double>(arraynew, double.Parse);
            string resultModel = "43934, 52503, 57177, 69658, 97031, 119931, 137133, 154175";
            string[] array = resultModel.Split(',');
            int[] iNums = Array.ConvertAll<string, int>(array, int.Parse);
            List<LineStru> ll = new List<LineStru>();
            LineStru lj = new LineStru();
            lj.name = "Installation";
            lj.list_d = iNumsnew;
            ll.Add(lj);


            string resultModel1 = "97031,97031,97031,97031,97031,97031,97031,97031";
            string[] array1 = resultModel1.Split(',');
            int[] iNums1 = Array.ConvertAll<string, int>(array1, int.Parse);
            List<LineStru> ll1 = new List<LineStru>();
            LineStru lj1 = new LineStru();
            lj1.name = "Other";
            lj1.list = iNums1;
            ll.Add(lj1);


            string jsonData = JsonConvert.SerializeObject(ll);
            return jsonData;
        }

        /// <summary>
        /// 访问安全的https网站时要SSL验证，用回调安全验证的方法，解决这个问题
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static StringBuilder getUrlResponse(string url)
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
