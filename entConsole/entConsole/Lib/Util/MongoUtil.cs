using MongoDB.Driver;
using System.Collections.Generic;

/******************************************************************
 * Author: miaoxin 
 * Date: 2017-11-15
 * Content: MongoUtil 工具类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// MongoUtil 工具类
    /// </summary>
    public class MongoUtil
    {
        //私有静态实例
        private static MongoUtil _instance = null;
        //锁
        private static readonly object padlock = new object();
        //客户端列表
        private static Dictionary<string, MongoClient> dic_Client = new Dictionary<string, MongoClient>();
        //锁
        private static readonly object diclock = new object();

        #region 私有构造函数
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private MongoUtil()
        {
        }
        #endregion

        #region 公有静态实例属性
        /// <summary>
        /// 公有静态实例属性
        /// </summary>
        public static MongoUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new MongoUtil();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region 取得客户端
        /// <summary>
        /// 取得客户端
        /// </summary>
        /// <param name="strConnStr">连接字符串（mongodb://finchina:finchina@10.15.97.174:9981/WeixinMsg）</param>
        /// <returns></returns>
        public MongoClient GetMongoClient(string strConnStr)
        {
            lock (diclock)
            {
                if (string.IsNullOrEmpty(strConnStr))
                {
                    return null;
                }

                //提取连接字符串32位MD5码，作为KEY
                string strKey = EncryptUtil.MD5Encrypt(strConnStr);

                if (!dic_Client.ContainsKey(strKey))
                {
                    MongoClient mongoClient = new MongoClient(strConnStr);

                    if (mongoClient != null)
                    {
                        dic_Client.Add(strKey, mongoClient);
                        return dic_Client[strKey];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return dic_Client[strKey];
                }
            }
        }
        #endregion

        #region 缓存客户端数量
        /// <summary>
        /// 缓存客户端数量
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            int dic_Count = 0;

            lock (diclock)
            {
                dic_Count = dic_Client.Count;
            }

            return dic_Count;
        }
        #endregion

        #region 清空所有缓存客户端
        /// <summary>
        /// 清空所有缓存客户端
        /// </summary>        
        public void Clear()
        {
            lock (diclock)
            {
                dic_Client.Clear();
            }
        }
        #endregion        
    }
}
