using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebConsole.Models.Model;
using WebConsole.Models.Common.HttpHelper;

namespace WebConsole.Controllers
{
    public class EventController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public string DrawLine(string id)
        {
            List<int> list1 = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] intList = list1.ToArray();


            HttpsGet httpget = new HttpsGet();
            string str = httpget.GetUrlResponse("http://data.gtimg.cn/flashdata/hushen/minute/sz000004.js?maxage=111").ToString();
            string[] array = str.Split("\\n\\\n");
            //string[] arraynew = new string[array0.Length - 3];
            List<double> list_double = new List<double>();
            int count = 60 * 4;
            for (int i = 2; i < count; i++)
            {
                if (i < array.Length - 3)
                {
                    list_double.Add(double.Parse(array[i].Split(' ')[1]));
                }
                else
                {
                    list_double.Add(double.NaN);
                }
                
            }

            //double[] iNumsnew = Array.ConvertAll<string, double>(arraynew, double.Parse);
            //string resultModel = "43934, 52503, 57177, 69658, 97031, 119931, 137133, 154175";
            //string[] array = resultModel.Split(',');
            //int[] iNums = Array.ConvertAll<string, int>(array, int.Parse);
            List<LineStru> ll = new List<LineStru>();
            LineStru lj = new LineStru();
            lj.name = "Stock";
            lj.list_d = list_double.ToArray();
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


    }
}
