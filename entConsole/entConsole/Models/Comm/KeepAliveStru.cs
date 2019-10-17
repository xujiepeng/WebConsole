/******************************************************************
 * Author: miaoxin 
 * Date: 2018-12-12 
 * Content: 存活接口结构
 ******************************************************************/

namespace entConsole.Models.Comm
{
    /// <summary>
    /// 存活接口结构
    /// </summary>
    public class KeepAliveStru
    {
        /// <summary>
        /// 1-正常;0-异常
        /// </summary>
        public int ok { get; set; }
        /// <summary>
        /// 数据库日期
        /// </summary>
        public string dbtime { get; set; }

        #region 构造函数
        public KeepAliveStru()
        {
            ok = 0;
            dbtime = "";
        }
        #endregion
    }
}
