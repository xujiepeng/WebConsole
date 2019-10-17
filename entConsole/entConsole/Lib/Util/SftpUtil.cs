using entConsole.Models.Conf;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;

/******************************************************************
 * Author: miaoxin 
 * Date: 2019-01-22 
 * Content: SFTP操作类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// SFTP操作类
    /// </summary>
    public class SftpUtil
    {
        #region 相关参数
        /// <summary>
        /// SftpClient
        /// </summary>
        private SftpClient sftpClient = null;
        /// <summary>
        /// SFTP地址
        /// </summary>
        private string strFtpUri = string.Empty;
        /// <summary>
        /// SFTP端口
        /// </summary>
        private int intFtpPort = 22;        
        /// <summary>
        /// SFTP用户名
        /// </summary>
        private string strFtpUserID = string.Empty;
        /// <summary>
        /// SFTP密码
        /// </summary>
        private string strFtpPassword = string.Empty;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ftpConfig">FTP配置封装</param>
        public SftpUtil(FtpStru ftpConfig)
        {
            this.strFtpUri = ftpConfig.ftpUri;
            this.intFtpPort = ftpConfig.ftpPort;
            this.strFtpUserID = ftpConfig.ftpUserID;
            this.strFtpPassword = ftpConfig.isPwdEncrypt? EncryptUtil.DesDecrypt_Default(ftpConfig.ftpPassword) : ftpConfig.ftpPassword;
            //创建Sftp客户端
            GetSftpClient();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strFtpUri">SFTP地址</param>
        /// <param name="strFtpUserID">SFTP用户名</param>
        /// <param name="strFtpPassword">SFTP密码</param>
        /// <param name="intFtpPort">SFTP端口</param>
        public SftpUtil(string strFtpUri, string strFtpUserID, string strFtpPassword, int intFtpPort = 22)
        {
            this.strFtpUri = strFtpUri;
            this.strFtpUserID = strFtpUserID;
            this.strFtpPassword = strFtpPassword;
            this.intFtpPort = intFtpPort;
            //创建Sftp客户端
            GetSftpClient();
        }
        #endregion
        
        #region 创建Sftp客户端
        /// <summary>
        /// 创建Sftp客户端
        /// </summary>
        private void GetSftpClient()
        {
            if (CheckPara())
            {
                try
                {
                    sftpClient = new SftpClient(strFtpUri, intFtpPort, strFtpUserID, strFtpPassword);
                }
                catch (Exception ex)
                {
                    Log4NetUtil.Error(this, "GetSftpClient->创建Sftp客户端异常:"+ex.ToString());
                }
            }
        }
        #endregion

        #region 连接SFTP
        /// <summary>
        /// 连接SFTP
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            bool result = false;
            if (sftpClient != null)
            {
                if (sftpClient.IsConnected)
                {
                    return true;
                }
                else
                {
                    sftpClient.Connect();
                    return true;
                }
            }
            return result;
        }
        #endregion

        #region 断开SFTP
        /// <summary>
        /// 断开SFTP
        /// </summary>
        public void DisConnect()
        {
            if (sftpClient != null)
            {
                if (sftpClient.IsConnected)
                {
                    sftpClient.Disconnect();
                }
            }
        }
        #endregion

        #region 校验参数
        /// <summary>
        /// 校验参数
        /// </summary>
        /// <returns></returns>
        private bool CheckPara()
        {
            bool boolResult = true;

            if (string.IsNullOrEmpty(strFtpUri))
            {
                Log4NetUtil.Error(this, "CheckPara->FtpUri为空");
                return false;
            }
            if (string.IsNullOrEmpty(strFtpUserID))
            {
                Log4NetUtil.Error(this, "CheckPara->FtpUserID为空");
                return false;
            }
            if (string.IsNullOrEmpty(strFtpPassword))
            {
                Log4NetUtil.Error(this, "CheckPara->FtpPassword为空");
                return false;
            }
            if (intFtpPort == 0 || intFtpPort == int.MaxValue || intFtpPort == int.MinValue)
            {
                Log4NetUtil.Error(this, "CheckPara->intFtpPort异常:" + intFtpPort.ToString());
                return false;
            }
            return boolResult;
        }
        #endregion

        #region 取得文件列表
        /// <summary>
        /// 取得文件列表
        /// </summary>
        /// <param name="remoteDic">远程目录</param>
        /// <param name="type">类型:file-文件,dic-目录</param>
        /// <returns></returns>
        public List<string> ListDirectory(string remoteDic, string type="file")
        {
            List<string> list = new List<string>();
            type = type.ToLower();

            try
            {
                if (Connect())
                {
                    var files = sftpClient.ListDirectory(remoteDic);
                    foreach (var file in files)
                    {
                        if (type == "file")
                        {
                            if (file.IsRegularFile)
                            {
                                list.Add(file.Name);
                            }
                        }
                        else if (type == "dic")
                        {
                            if (file.IsDirectory)
                            {
                                list.Add(file.Name);
                            }
                        }
                        else
                        {
                            list.Add(file.Name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "ListDirectory->取得文件列表 异常:" + ex.ToString());
            }
            finally
            {
                DisConnect();
            }

            return list;
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地路径(@"D:\abc.txt")</param>
        /// <param name="remoteDic">远端目录("/test")</param>
        /// <returns></returns>
        public bool UploadFile(string localPath, string remoteDic)
        {
            bool boolResult = false;
            FileInfo fileInfo = null;

            try
            {
                //本地路径校验
                if (!File.Exists(localPath))
                {
                    Log4NetUtil.Error(this, "UploadFile->本地文件不存在:" + localPath);
                    return boolResult;
                }
                else
                {
                    fileInfo = new FileInfo(localPath);
                }
                //远端路径校验
                if (string.IsNullOrEmpty(remoteDic))
                {
                    remoteDic = "/";
                }
                if (!remoteDic.StartsWith("/"))
                {
                    remoteDic = "/" + remoteDic;
                }
                if (!remoteDic.EndsWith("/"))
                {
                    remoteDic += "/";
                }

                //拼接远端路径
                remoteDic += fileInfo.Name;

                if (Connect())
                {
                    using (FileStream fs = fileInfo.OpenRead())
                    {
                        //重名覆盖
                        sftpClient.UploadFile(fs, remoteDic, true);
                    }
                    
                    boolResult = true;
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "UploadFile->上传文件 异常:" + ex.ToString() + "|*|localPath:" + localPath);
            }
            finally
            {
                DisConnect();
            }

            return boolResult;
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="localDic">本地目录(@"D:\test")</param>
        /// <param name="remotePath">远程路径("/test/abc.txt")</param>
        /// <returns></returns>
        public bool DownloadFile(string localDic, string remotePath)
        {
            bool boolResult = false;
            string strFileName = string.Empty;

            try
            {
                //本地目录不存在，则自动创建
                if (!Directory.Exists(localDic))
                {
                    Directory.CreateDirectory(localDic);
                }
                //取下载文件的文件名
                strFileName = Path.GetFileName(remotePath);
                //拼接本地路径
                localDic = Path.Combine(localDic, strFileName);

                if (Connect())
                {
                    var byt = sftpClient.ReadAllBytes(remotePath);

                    File.WriteAllBytes(localDic, byt);
                    
                    boolResult = true;
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "DownloadFile->下载文件 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
            }
            finally
            {
                DisConnect();
            }

            return boolResult;
        }
        #endregion

        #region 判断文件或目录是否存在
        /// <summary>
        /// 判断文件或目录是否存在
        /// </summary>
        /// <param name="remotePath">远程路径("/test/abc.txt","/test")</param>
        /// <returns></returns>
        public bool IsExists(string remotePath)
        {
            bool boolResult = false;

            try
            {
                if (Connect())
                {
                    boolResult = sftpClient.Exists(remotePath);
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "IsExists->判断文件或目录是否存在 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
            }
            finally
            {
                DisConnect();
            }

            return boolResult;
        }
        #endregion

        #region 删除文件或目录
        /// <summary>
        /// 删除文件或目录
        /// </summary>
        /// <param name="remotePath">远程路径("/test/abc.txt","/test")</param>
        /// <returns></returns>
        public bool Delete(string remotePath)
        {
            bool boolResult = false;

            try
            {
                if (Connect())
                {
                    sftpClient.Delete(remotePath);

                    boolResult = true;
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "Delete->删除文件或目录 异常:" + ex.ToString() + "|*|remotePath:" + remotePath);
            }
            finally
            {
                DisConnect();
            }

            return boolResult;
        }
        #endregion

        #region 新建目录
        /// <summary>
        /// 新建目录
        /// </summary>
        /// <param name="remoteDic">远程目录("/test")</param>
        /// <returns></returns>
        public bool MakeDir(string remoteDic)
        {
            bool boolResult = false;

            try
            {
                if (Connect())
                {
                    sftpClient.CreateDirectory(remoteDic);

                    boolResult = true;
                }
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(this, "MakeDir->新建目录 异常:" + ex.ToString() + "|*|remoteDic:" + remoteDic);
            }
            finally
            {
                DisConnect();
            }

            return boolResult;
        }
        #endregion

        #region 清理
        /// <summary>
        /// 清理
        /// </summary>
        public void Clean()
        {
            //断开SFTP
            DisConnect();

            if (sftpClient != null)
            {
                sftpClient.Dispose();
            }
        }
        #endregion
    }
}
