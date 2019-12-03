using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebConsole.Controllers
{
    public class EventController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public string getdata(string id)
        {
            string resultModel = "43934, 52503, 57177, 69658, 97031, 119931, 137133, 154175";
            string[] array = resultModel.Split(',');
            int[] iNums = Array.ConvertAll<string, int>(array, int.Parse);
            List<listjson> ll = new List<listjson>();
            listjson lj = new listjson();
            lj.name = "Installation";
            lj.list = iNums;
            ll.Add(lj);


            string resultModel1 = "97031,97031,97031,97031,97031,97031,97031,97031";
            string[] array1 = resultModel1.Split(',');
            int[] iNums1 = Array.ConvertAll<string, int>(array1, int.Parse);
            List<listjson> ll1 = new List<listjson>();
            listjson lj1 = new listjson();
            lj1.name = "Other";
            lj1.list = iNums1;
            ll.Add(lj1);


            string jsonData = JsonConvert.SerializeObject(ll);
            return jsonData;
        }
    }

    public class listjson
    {
        public string name;
        public int[] list;
    }
}
