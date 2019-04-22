using System.Collections.Generic;

/******************************************************************
 * Author: miaoxin 
 * Date: 2018-10-16
 * Content: Object缓存类
 ******************************************************************/

namespace entConsole.Lib.Util
{
    /// <summary>
    /// Object缓存类
    /// </summary>
    public class ObjCacheUtil
    {
        //缓存数据字典
        private Dictionary<string, object> dic_Cache = new Dictionary<string, object>();

        //数据字典私有静态实例
        private static volatile ObjCacheUtil instance = null;
        private static object padlock = new object();
        private static object diclock = new object();

        #region 私有构造函数
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private ObjCacheUtil()
        {
        }
        #endregion

        #region 公有静态实例属性
        /// <summary>
        /// 公有静态实例属性
        /// </summary>
        public static ObjCacheUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ObjCacheUtil();
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
        /// 取得值,无Key对应时返回null
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <returns>strValue</returns>
        public object GetValue(string strKey)
        {
            object objValue = null;

            if (string.IsNullOrEmpty(strKey))
            {
                return null;
            }

            if (!dic_Cache.ContainsKey(strKey))
            {
                return null;
            }

            lock (diclock)
            {
                objValue = dic_Cache[strKey];
            }

            return objValue;
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

        #region 新增键值对
        /// <summary>
        /// 新增键值对，Key存在时返回false
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <returns>bool</returns>
        public bool AddValue(string strKey, object objValue)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            if (dic_Cache.ContainsKey(strKey))
            {
                return false;
            }

            lock (diclock)
            {
                dic_Cache.Add(strKey, objValue);
            }

            return true;
        }
        #endregion

        #region 新增/覆盖键值对
        /// <summary>
        /// 新增/覆盖键值对
        /// </summary>
        /// <param name="strKey">Key</param>
        /// <param name="objValue">Value</param>
        /// <returns>bool</returns>
        public bool AddUpValue(string strKey, object objValue)
        {
            if (string.IsNullOrEmpty(strKey))
            {
                return false;
            }

            lock (diclock)
            {
                if (dic_Cache.ContainsKey(strKey))
                {
                    dic_Cache[strKey] = objValue;
                }
                else
                {
                    dic_Cache.Add(strKey, objValue);
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

            if (!dic_Cache.ContainsKey(strKey))
            {
                return false;
            }

            lock (diclock)
            {
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
