using System.Collections.Generic;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-22
 * Content: 通用缓存类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// 通用缓存类
    /// </summary>
    public class CommCache
    {
        //缓存数据字典
        private Dictionary<string, string> dic_Cache = new Dictionary<string, string>();

        //数据字典私有静态实例
        private static volatile CommCache instance = null;
        private static object padlock = new object();
        private static object diclock = new object();

        #region 私有构造函数
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private CommCache()
        {
        }
        #endregion

        #region 公有静态实例属性
        /// <summary>
        /// 公有静态实例属性
        /// </summary>
        public static CommCache Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new CommCache();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion

        #region 是否包含指定的键
        /// <summary>
        /// 是否包含指定的键
        /// </summary>
        /// <param name="strKey">strKey</param>
        /// <returns>bool</returns>
        public bool IsContainsKey(string strKey)
        {
            bool boolIsContainsKey = false;

            if (string.IsNullOrEmpty(strKey))
            {
                return boolIsContainsKey;
            }

            lock (diclock)
            {
                boolIsContainsKey = dic_Cache.ContainsKey(strKey);
            }

            return boolIsContainsKey;
        }
        #endregion

        #region 取得值
        /// <summary>
        /// 取得值
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>strValue</returns>
        public string GetValue(string strKey)
        {
            string strValue = string.Empty;

            if (string.IsNullOrEmpty(strKey))
            {
                return null;
            }

            lock (diclock)
            {
                if (!dic_Cache.ContainsKey(strKey))
                {
                    return null;
                }
                strValue = dic_Cache[strKey];
            }

            return strValue;
        }
        #endregion

        #region 键值对数量
        /// <summary>
        /// 键值对数量
        /// </summary>
        /// <returns>键值对数量</returns>
        public int Count()
        {
            int dic_Count = 0;

            lock (diclock)
            {
                dic_Count = dic_Cache.Count;
            }

            return dic_Count;
        }
        #endregion

        #region 添加键值对
        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns>bool</returns>
        public bool AddValue(string strKey, string strValue)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            lock (diclock)
            {
                if (dic_Cache.ContainsKey(strKey))
                {
                    return false;
                }
                dic_Cache.Add(strKey, strValue);
            }

            return true;
        }
        #endregion

        #region 新增/覆盖键值对
        /// <summary>
        /// 新增/覆盖键值对
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="strValue">Value</param>
        /// <returns>bool</returns>
        public bool AddUpValue(string strKey, string strValue)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            lock (diclock)
            {
                if (dic_Cache.ContainsKey(strKey))
                {
                    dic_Cache[strKey] = strValue;
                }
                else
                {
                    dic_Cache.Add(strKey, strValue);
                }
            }

            return true;
        }
        #endregion

        #region 移除指定键
        /// <summary>
        /// 移除指定键
        /// </summary>
        /// <param name="strKey">Key</param>        
        /// <returns>bool</returns>
        public bool Remove(string strKey)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            lock (diclock)
            {
                if (!dic_Cache.ContainsKey(strKey))
                {
                    return false;
                }
                dic_Cache.Remove(strKey);
            }

            return true;
        }
        #endregion

        #region 清空所有键值对
        /// <summary>
        /// 清空所有键值对
        /// </summary>        
        public void Clear()
        {
            lock (diclock)
            {
                dic_Cache.Clear();
            }
        }
        #endregion        
    }
}
