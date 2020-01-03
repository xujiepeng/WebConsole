using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common.FileHelper
{
    public class TxtHelper
    {
        public void Write_txt(string log)
        {
            string path = "D:\\YSJS-BK-Finance\\logs1\\";//文件路径
            string logFileName = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd") + "Log.log");//全部路径
            if (!Directory.Exists(path))//若文件夹不存在则新建文件夹   
                Directory.CreateDirectory(path); //新建文件夹   

            File.AppendAllText(logFileName, DateTime.Now.ToString() + " ");
            File.AppendAllText(logFileName, log);
            File.AppendAllText(logFileName, Convert.ToChar(13).ToString());
            File.AppendAllText(logFileName, Convert.ToChar(10).ToString());

        }
    }
}
