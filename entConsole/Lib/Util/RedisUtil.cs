using StackExchange.Redis;
using System.Collections.Generic;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-31
 * Content: RedisUtil 工具类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// RedisUtil 工具类
    /// </summary>
    public class RedisUtil
    {
        //私有静态实例
        private static RedisUtil _instance = null;
        //锁
        private static readonly object padlock = new object();
        //客户端列表
        private static Dictionary<string, ConnectionMultiplexer> dic_Client = new Dictionary<string, ConnectionMultiplexer>();
        //锁
        private static readonly object diclock = new object();

        #region 私有构造函数
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private RedisUtil()
        {
        }
        #endregion

        #region 公有静态实例属性
        /// <summary>
        /// 公有静态实例属性
        /// </summary>
        public static RedisUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new RedisUtil();
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
        /// <param name="strConnStr">连接字符串（10.15.43.32:6379,password=finchina）</param>
        /// <returns></returns>
        public ConnectionMultiplexer GetRedisClient(string strConnStr)
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
                    ConnectionMultiplexer redisClient = ConnectionMultiplexer.Connect(strConnStr);

                    if (redisClient != null)
                    {
                        dic_Client.Add(strKey, redisClient);
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
