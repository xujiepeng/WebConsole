using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

/******************************************************************
 * Author: miaoxin 
 * Date: 2019-01-22
 * Content: 获取系统信息
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 获取系统信息
    /// </summary>
    public class SysInfoUtil
    {
        #region 取得网卡Mac地址
        /// <summary>
        /// 取得网卡Mac地址
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMacAddress()
        {
            List<string> _list = new List<string>();
            string mac = string.Empty;

            try
            {
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    mac = ni.GetPhysicalAddress().ToString();
                    if (!string.IsNullOrEmpty(mac))
                    {
                        _list.Add(mac);
                    }
                }
            }
            catch
            { }

            return _list;
        }

        /// <summary>
        /// 取得网卡Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacStr()
        {
            StringBuilder sb = new StringBuilder();
            List<string> _list = GetMacAddress();

            foreach (string item in _list)
            {
                sb.Append(item);
                sb.Append("|");
            }
            return sb.ToString();
        }
        #endregion

        #region 取得IP地址
        /// <summary>
        /// 取得IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            string ip = string.Empty;
            try
            {
                ip = NetworkInterface.GetAllNetworkInterfaces()
                  .Select(p => p.GetIPProperties())
                  .SelectMany(p => p.UnicastAddresses)
                  .Where(p => p.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork && !System.Net.IPAddress.IsLoopback(p.Address))
                  .FirstOrDefault()?.Address.ToString();
            }
            catch
            {
                ip = "127.0.0.1";
            }           

            return ip;
        }
        #endregion
    }
}
