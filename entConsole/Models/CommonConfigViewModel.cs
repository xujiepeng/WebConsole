using entConsole.Lib.Util;
using entConsole.Models.Conf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace entConsole.Models
{
    public class CommonConfigViewModel
    {
        #region 属性
        /// <summary>
        /// 当前table页显示数据
        /// </summary>
        public List<CommonConfigStru> dataInfo = new List<CommonConfigStru>();
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
        public List<CommonConfigStru> GetDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            //查询数据总数
            List<CommonConfigStru> tempDataInfo = new List<CommonConfigStru>();
            CommonMethod comm = new CommonMethod();
            string errorMsg = string.Empty;
            string sql = @"SELECT * FROM KeepAlive";

            DbConnStru SqlitePath = options.DbConnList.FirstOrDefault(o => o.key == "SqliteConn_1");

            var list = comm.Select<CommonConfigStru>(sql, SqlitePath);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                return null;
            }

            foreach (CommonConfigStru item in list)
            {
                tempDataInfo.Add(item);
            }

            sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
            sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
            sqlDic["clickpagenow"] = pageClick.ToString();

            dataInfo = comm.TablePage<CommonConfigStru>(tempDataInfo, ref sqlDic);
            return dataInfo;
        }
        #endregion



        #region 读取XML文件
        /// <summary>
        /// 读取XML文件
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<CommonConfigStru> GetSftpDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodeList = null;
            XmlNode xmlNode = null;
            string str_Temp = string.Empty;
            dataInfo = new List<CommonConfigStru>();

            //XML配置文件路径
            string str_ConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            //string str_ConfigFilePath = options.AttriList.FirstOrDefault(l => l.key == "XMLPath").value;
            if (!File.Exists(str_ConfigFilePath))
            {
                Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件不存在，str_ConfigFilePath=" + str_ConfigFilePath);
                return new List<CommonConfigStru>();
            }

            //XML配置部分
            try
            {
                xmlDoc.Load(str_ConfigFilePath);
                CommonConfigStru ftpConfig = new CommonConfigStru();
                #region 加载Ftp配置
                xmlNode = xmlDoc.SelectSingleNode("/Root/CommConfig/Sftp");
                if (xmlNode != null)
                {
                    
                    ftpConfig.Name = xmlNode.SelectSingleNode("Name").InnerText.Trim();
                    ftpConfig.ftpUri = xmlNode.SelectSingleNode("FtpUri").InnerText.Trim();
                    ftpConfig.ftpPort = ConvertUtil.ParseInt(xmlNode.SelectSingleNode("FtpPort").InnerText.Trim(), 22).ToString();
                    ftpConfig.ftpUserID = xmlNode.SelectSingleNode("FtpUserID").InnerText.Trim();
                    ftpConfig.ftpPassword = xmlNode.SelectSingleNode("FtpPassword").InnerText.Trim();
                    ftpConfig.isEncrypt = xmlNode.SelectSingleNode("IsEncrypt").InnerText.Trim().ToLower() == "true" ? true : false;
                    if (ftpConfig.isEncrypt)
                    {
                        //解密
                        ftpConfig.ftpPassword = EncryptUtil.DesDecrypt_Default(ftpConfig.ftpPassword);
                    }
                }
                else
                {
                    Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件格式异常,请检查节点:/Root/CommConfig/Sftp");
                    return new List<CommonConfigStru>();
                }
                #endregion


                #region 加载Ftp配置
                xmlNode = xmlDoc.SelectSingleNode("/Root/CommConfig/HttpsAddr");
                if (xmlNode != null)
                {
                    CommonConfigStru httpsAddrConfig1 = new CommonConfigStru();
                    ftpConfig.httpsAddr = xmlNode.InnerText.Trim();

                    if (!string.IsNullOrEmpty(ftpConfig.httpsAddr))
                    {
                        //Name 唯一
                        if (dataInfo.FirstOrDefault<CommonConfigStru>(t => t.Name == ftpConfig.httpsAddr) == null)
                        {
                            dataInfo.Add(ftpConfig);
                        }
                    }

                    
                }
                else
                {
                    Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件格式异常,请检查节点:/Root/CommConfig/Sftp");
                    return new List<CommonConfigStru>();
                }
                #endregion

                sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
                sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
                sqlDic["clickpagenow"] = pageClick.ToString();

                return dataInfo;

            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "EntDtConf->加载XML配置文件异常:" + ex.ToString());
                return new List<CommonConfigStru>();
            }
            finally
            {
                xmlDoc = null;
                xmlNodeList = null;
                str_Temp = null;
            }
        }
        #endregion

        #region 读取XML文件
        /// <summary>
        /// 读取XML文件
        /// </summary>
        /// <param name="options"></param>
        /// <param name="pageclick">当前点击页</param>
        /// <param name="sqlDic">过滤器条件</param>
        /// <returns></returns>
        public List<CommonConfigStru> GetHttpsAddrDataInfo(CommConf options, int pageClick, ref Dictionary<string, string> sqlDic)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNodeList xmlNodeList = null;
            XmlNode xmlNode = null;
            string str_Temp = string.Empty;
            dataInfo = new List<CommonConfigStru>();

            //XML配置文件路径
            string configFilePath = options.AttriList.FirstOrDefault(l => l.key == "XMLPath").value;
            if (!File.Exists(configFilePath))
            {
                Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件不存在，str_ConfigFilePath=" + configFilePath);
                return new List<CommonConfigStru>();
            }

            //XML配置部分
            try
            {
                xmlDoc.Load(configFilePath);
                #region 加载Ftp配置
                xmlNode = xmlDoc.SelectSingleNode("/Root/CommConfig/HttpsAddr");
                if (xmlNode != null)
                {
                    CommonConfigStru httpsAddrConfig = new CommonConfigStru();
                    httpsAddrConfig.httpsAddr = xmlNode.InnerText.Trim();

                    if (!string.IsNullOrEmpty(httpsAddrConfig.httpsAddr))
                    {
                        //Name 唯一
                        if (dataInfo.FirstOrDefault<CommonConfigStru>(t => t.Name == httpsAddrConfig.httpsAddr) == null)
                        {
                            dataInfo.Add(httpsAddrConfig);
                        }
                    }

                    sqlDic["PageShowNum"] = options.AttriList.FirstOrDefault(o => o.key == "PageShowNum").value;
                    sqlDic["allowPageNum"] = options.AttriList.FirstOrDefault(l => l.key == "allowPageNum").value;
                    sqlDic["clickpagenow"] = pageClick.ToString();

                    return dataInfo;
                }
                else
                {
                    Log4NetUtil.Error(this, "EntDtConf->config.xml 配置文件格式异常,请检查节点:/Root/CommConfig/Sftp");
                    return new List<CommonConfigStru>();
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "EntDtConf->加载XML配置文件异常:" + ex.ToString());
                return new List<CommonConfigStru>();
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
