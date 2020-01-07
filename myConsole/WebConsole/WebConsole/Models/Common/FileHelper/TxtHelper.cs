using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebConsole.Models.Common.FileHelper
{
    public class TxtHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="filename">文件名</param>
        /// <param name="context">日志内容</param>
        public static void Write_txt(string path, string filename, string context)
        {
            string logFileName = Path.Combine(path, filename);
            if (!Directory.Exists(path))//若文件夹不存在则新建文件夹
            {
                Directory.CreateDirectory(path); //新建文件夹
            }
            else
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path); //新建文件夹
            }
            if (!Directory.Exists(path))//若文件夹不存在则新建文件夹
            {
                Thread.Sleep(100);
                Directory.CreateDirectory(path); //新建文件夹
            }
            File.AppendAllText(logFileName, context);
            File.AppendAllText(logFileName, Convert.ToChar(13).ToString());
            File.AppendAllText(logFileName, Convert.ToChar(10).ToString());

        }
    }
}
