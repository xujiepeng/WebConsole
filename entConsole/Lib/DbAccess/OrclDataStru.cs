using Oracle.ManagedDataAccess.Client;

/******************************************************************
 * Author: miaoxin 
 * Date: 2019-02-19 
 * Content: Oracle参数数据结构
 ******************************************************************/

namespace entConsole.Lib.DbAccess
{
    /// <summary>
    /// Oracle参数数据结构
    /// </summary>
    public class OrclDataStru
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrclDataStru()
        {
            dbType = OracleDbType.Varchar2;
        }
        #endregion

        /// <summary>
        /// Oracle字段类型
        /// </summary>
        public OracleDbType dbType { get; set; }
        /// <summary>
        /// Oracle字段数据数组
        /// </summary>
        public object arrParam { get; set; }
    }
}
