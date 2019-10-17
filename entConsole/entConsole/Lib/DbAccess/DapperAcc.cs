using entConsole.Lib.Util;
using entConsole.Models.Conf;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;

/******************************************************************
 * Author: miaoxin 
 * Date: 2019-03-26 
 * Content: DapperAcc数据库访问
 ******************************************************************/

namespace entConsole.Lib.DbAccess
{
    /// <summary>
    /// DapperAcc数据库访问
    /// </summary>
    public class DapperAcc
    {
        #region 获取数据库链接
        /// <summary>
        /// 获取数据库链接
        /// </summary>
        /// <param name="connConfig">数据库链接配置结构</param>
        /// <returns></returns>
        public static IDbConnection GetConnection(DbConnStru connConfig)
        {
            IDbConnection conn = null;

            if (string.IsNullOrEmpty(connConfig.connStr))
            {
                return null;
            }

            try
            {
                switch (connConfig.dbType.ToLower().Trim())
                {
                    case "sqlserver":
                        conn = new SqlConnection(connConfig.connStr);
                        break;
                    case "oracle":
                        conn = new OracleConnection(connConfig.connStr);
                        break;
                    case "mysql":
                        conn = new MySqlConnection(connConfig.connStr);
                        break;
                    case "sqlite":
                        conn = new SQLiteConnection(connConfig.connStr);
                        break;
                    default:
                        conn = new SqlConnection(connConfig.connStr);
                        break;
                }

                //打开数据库链接
                conn.Open();
            }
            catch (Exception ex)
            {
                Log4NetUtil.Error(typeof(DapperAcc), "ConnStr:" + connConfig.connStr + " ErrorMsg:" + ex.ToString());
            }
            return conn;
        }
        #endregion
    }
}
