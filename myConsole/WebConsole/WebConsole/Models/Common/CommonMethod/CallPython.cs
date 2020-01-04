using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebConsole.Models.Common
{
    public class CallPython
    {
        public static string RunStr(string script)
        {
            string Path = "E:\\MyGit\\TEST\\CSharpCallPython\\bin\\Debug";//脚本文件路径
            string Filename = "test2";//执行脚本文件名称
            string functionname = "add(2,4)";//脚本中要调用的方法
            //1.调用现有的python脚本
            string cmd = string.Format("-c \"import sys;sys.path.append('{0}');import {1};print({1}.{2})\"", Path, Filename, functionname);
            //2.直接动态传入要执行的sql脚本
            cmd = string.Format("-c \"import sys;print(sys.path);a=2;b=3;c=a+b;print('result='+str(c));\"");
            cmd = string.Format("-c \"{0};\"", script);
            //Console.WriteLine("请输入");
            //string input = Console.ReadLine();
            //cmd = string.Format("-c \""+input+"\"");

            string result = CallCmd.run_cmd("python.exe", cmd);
            return result;
        }


        public static string RunFile()
        {
            string path = "E:\\MyGit\\TEST\\CSharpCallPython\\bin\\Debug\\test3.py";
            string para1 = "\"Form C#:\"";
            string para2 = "\"Form C++++:\"";
            string strcmd = string.Format("{0} {1} {2}", path, para1, para2);
            string cmdresult = CallCmd.run_cmd("python.exe", strcmd);
            return cmdresult;
        }
        
    }
}
