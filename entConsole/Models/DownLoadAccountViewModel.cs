using entConsole.Lib.Util;
using entConsole.Models.Conf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace entConsole.Models
{
    public class DownLoadAccountViewModel
    {
        #region 属性
        /// <summary>
        /// 当前table页显示数据
        /// </summary>
        public List<DownLoadAccountStru> dataInfo = new List<DownLoadAccountStru>();
        /// <summary>
        /// Url参数列表
        /// </summary>
        public Dictionary<string, string> paraDic = new Dictionary<string, string>();

        public string AppBasePath { get; private set; }
        #endregion


        #region Sqlite加载显示的数据
        /// <summary>
        /// Sqlite加载显示的数据
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<DownLoadAccountStru> GetDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            //查询数据总数
            List<DownLoadAccountStru> tempDataInfo = new List<DownLoadAccountStru>();
            CommonMethod comm = new CommonMethod();
            string errorMsg = string.Empty;
            string sql = @"SELECT * FROM KeepAlive";

            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list = comm.Select<DownLoadAccountStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (DownLoadAccountStru item in list)
            {
                tempDataInfo.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();

            dataInfo = comm.TablePage<DownLoadAccountStru>(tempDataInfo, ref sqlDic);
            return dataInfo;
        }
        #endregion


        #region Sqlite加载显示的数据
        /// <summary>
        /// Sqlite加载显示的数据
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<DownLoadAccountStru> GetXMLDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodeList = null;
            XmlNode xmlNode = null;
            string str_Temp = string.Empty;

            //XML配置文件路径
            string str_ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            //string str_ConfigFilePath = options.AttriList.FirstOrDefault(l => l.key == "XMLPath").value;
            if (!File.Exists(str_ConfigFilePath))
            {
                Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件不存在，str_ConfigFilePath=" + str_ConfigFilePath);
                return new List<DownLoadAccountStru>();
            }
            
            //XML配置部分
            try
            {
                xmlDoc.Load(str_ConfigFilePath);

                #region 加载 UserList
                xmlNodeList = xmlDoc.SelectNodes("/Root/UserList/User");
                if (xmlNodeList != null)
                {
                    //List<DownLoadAccountStru> list_DownLoadAccount = new List<DownLoadAccountStru>();
                    foreach (XmlNode node in xmlNodeList)
                    {
                        //用户账号配置
                        DownLoadAccountStru userConfig = new DownLoadAccountStru();
                        userConfig.userName = node.SelectSingleNode("UserName").InnerText.Trim();
                        userConfig.Pwd = node.SelectSingleNode("Pwd").InnerText.Trim();
                        userConfig.pwdIsEncrypt = node.SelectSingleNode("IsEncrypt").InnerText.Trim().ToLower() == "true" ? true : false;
                        //解密
                        if (userConfig.pwdIsEncrypt)
                        {
                            userConfig.Pwd = EncryptUtil.DesDecrypt_Default(userConfig.Pwd);
                        }

                        //用户数据库链接配置
                        XmlNode connNode = node.SelectSingleNode("Conn");
                        if (connNode != null)
                        {
                            userConfig.Name = connNode.SelectSingleNode("Name").InnerText.Trim();
                            userConfig.DbType = connNode.SelectSingleNode("DbType").InnerText.Trim();
                            userConfig.ConnStr = connNode.SelectSingleNode("ConnStr").InnerText.Trim();
                            userConfig.connStrIsEncrypt = connNode.SelectSingleNode("IsEncrypt").InnerText.Trim().ToLower() == "true" ? true : false;
                            //解密
                            if (userConfig.connStrIsEncrypt)
                            {
                                userConfig.ConnStr = EncryptUtil.DesDecrypt_Default(userConfig.ConnStr);
                            }
                            if (node.SelectSingleNode("Encoding") != null)
                            {
                                userConfig.Encoding = node.SelectSingleNode("Encoding").InnerText.Trim();
                            }

                            
                        }
                        else
                        {
                            Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件格式异常,请检查节点:/Root/UserList/User/Conn");
                            continue;
                        }
                        
                        if (!string.IsNullOrEmpty(userConfig.userName))
                        {
                            //UserName 唯一
                            if (dataInfo.FirstOrDefault<DownLoadAccountStru>(t => t.userName == userConfig.userName) == null)
                            {
                                dataInfo.Add(userConfig);
                            }
                        }
                    }
                    sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
                    sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
                    sqlDic["clickpagenow"] = pageClick.ToString();

                    return dataInfo;
                }
                else
                {
                    Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件格式异常,请检查节点:/Root/UserList/User");
                    return new List<DownLoadAccountStru>();
                }
                #endregion
                
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "EntDtConf->加载XML配置文件异常:" + ex.ToString());
                return new List<DownLoadAccountStru>();
            }
            finally
            {
                xmlDoc = null;
                xmlNodeList = null;
                str_Temp = null;
            }
        }
        #endregion
    }
}
