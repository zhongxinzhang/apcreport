using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace APCReport.SQLserver
{
     // <summary>
     // SqlHelper类是专门提供给广大用户用于高性能、可升级和最佳练习的sql数据操作
     // </summary>
     public class SqlHelper
    {
        //创建数据库连接
         public static readonly string connString = ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
         //public static readonly string connString = ConfigurationManager.ConnectionStrings["APCSQLServer"].ConnectionString;
 
        /// <summary>
        /// 等待执行SQL语句的SqlCommand对象
        /// </summary>
        /// <param name="conn">SqlConnection对象，不允许为空</param>
        /// <param name="comm">SqlCommand对象，不允许为空</param>
        /// <param name="commType">SqlCommand类型，不允许为空</param>
        /// <param name="commText">SqlCommand文本或SQL语句，不允许为空</param>
        /// <param name="commParam">SqlParameter对象，允许为空</param>
        private static void PrepareCommand(SqlConnection conn, SqlCommand comm, CommandType commType, string commText, SqlParameter[] commParam){
            //判断数据库连接是否打开
            if (conn.State != ConnectionState.Open){
                //打开数据库连接
                conn.Open();
            }
            //设置SqlCommand的连接
            comm.Connection = conn;
            //设置SqlCommand的文本
            comm.CommandText = commText;
            //设置SqlCommand的类型
            comm.CommandType = commType;
            //判断SqlParameter是否为空
            if (commParam != null){
                //循环填充数据
                foreach (SqlParameter param in commParam){
                    comm.Parameters.Add(param);
                }
            }
        }
 
        /// <summary>
        /// 等待执行存储过程的SqlCommand对象
        /// </summary>
        /// <param name="conn">SqlConnection对象，不允许为空</param>
        /// <param name="comm">SqlCommand对象，不允许为空</param>
        /// <param name="commName">存储过程名称</param>
        /// <param name="commParam">SqlParameter对象，允许为空</param>
        private static void PrepareCommand(SqlConnection conn, SqlCommand comm, string commName, params object[] commParam){
            //判断数据库连接是否打开
            if (conn.State != ConnectionState.Open){
                //打开数据库连接
                conn.Open();
            }
            //设置SqlCommand的连接
            comm.Connection = conn;
            //设置SqlCommand的存储过程名称
            comm.CommandText = commName;
            //设置SqlCommand的类型
            comm.CommandType = CommandType.StoredProcedure;
            //获取存储过程的参数
            SqlCommandBuilder.DeriveParameters(comm);
            //移除Return_Value 参数
            comm.Parameters.RemoveAt(0);
            //判断SqlParameter是否为空
            if (commParam != null){
                //循环赋值
                for (int i = 0; i < comm.Parameters.Count; i++){
                    comm.Parameters[i].Value = commParam[i];
                }
            }
        }
 
        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commType">命令类型</param>
        /// <param name="commText">SQL语句 / 参数化SQL语句</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string connString, CommandType commType, string commText, params SqlParameter[] commParam){
            //创建SqlCommand命令
            SqlCommand comm = new SqlCommand();
            //使用Using提前释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //执行方法并传递参数
                PrepareCommand(conn, comm, commType, commText, commParam);
                //执行操作并接收结果
                int val = comm.ExecuteNonQuery();
                //返回结果
                return val;
            }
        }
 
        /// <summary>
        /// 执行SQL存储过程
        /// 注意：不能执行有输出（OUT）参数的存储过程
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commName">存储过程名称</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>受影响的行数</returns>
        public static int ExecuteNonQuery(string connString, string commName, params object[] commParam){
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //执行方法并传递参数
                PrepareCommand(conn, comm, commName, commParam);
                //执行操作并接收结果
                int val = comm.ExecuteNonQuery();
                //返回结果
                return val;
            }
        }
 
        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commType">命令类型</param>
        /// <param name="commText">SQL语句 / 参数化SQL语句</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string connString, CommandType commType, string commText, params SqlParameter[] commParam){
            //创建数据库连接
            SqlConnection conn = new SqlConnection(connString);
            try{
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //执行方法并传递参数
                PrepareCommand(conn, comm, commType, commText, commParam);
                //创建SqlDataReader接收数据
                SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                //返回SqlDataReader对象
                return reader;
            }catch{
                //关闭数据库连接
                conn.Close();
                //抛出异常
                throw;
            }
        }
 
        /// <summary>
        /// 执行SQL存储过程
        /// 注意：不能执行有输出（OUT）参数的存储过程
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commName">存储过程名称</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>SqlDataReader对象</returns>
        public static SqlDataReader ExecuteReader(string connString, string commName, params object[] commParam){
            //创建数据库连接
            SqlConnection conn = new SqlConnection(connString);
            try{
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //执行方法并传递参数
                PrepareCommand(conn, comm, commName, commParam);
                //创建SqlDataReader接收数据
                SqlDataReader reader = comm.ExecuteReader(CommandBehavior.CloseConnection);
                //返回SqlDataReader对象
                return reader;
            }catch{
                //关闭数据库连接
                conn.Close();
                //抛出异常
                throw;
            }
        }
 
        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commType">命令类型</param>
        /// <param name="commText">SQL语句 / 参数化SQL语句</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(string connString, CommandType commType, string commText, params SqlParameter[] commParam){
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //执行方法并传递参数
                PrepareCommand(conn, comm, commType, commText, commParam);
                //使用Using释放SqlDataAdapter资源
                using (SqlDataAdapter da = new SqlDataAdapter(comm)){
                    //初始化DataSet
                    DataSet ds = new DataSet();
                    //执行方法并传递参数
                    da.Fill(ds);
                    //返回DataSet对象
                    return ds;
                }
            }
        }
 
        /// <summary>
        /// 执行SQL存储过程
        /// 注意：不能执行有输出（OUT）参数的存储过程
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commName">存储过程名称</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>DataSet对象</returns>
        public static DataSet ExecuteDataSet(string connString, string commName, params object[] commParam){
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //执行方法并传递参数
                PrepareCommand(conn, comm, commName, commParam);
                //使用Using释放SqlDataAdapter资源
                using (SqlDataAdapter da = new SqlDataAdapter(comm)){
                    //初始化DataSet
                    DataSet ds = new DataSet();
                    //执行方法并传递参数
                    da.Fill(ds);
                    //返回DataSet对象
                    return ds;
                }
            }
        }
 
        /// <summary>
        /// 执行SQL语句命令
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commType">命令类型</param>
        /// <param name="commText">SQL语句 / 参数化SQL语句</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>执行结果对象</returns>
        public static object ExecuteScalar(string connString, CommandType commType, string commText, params SqlParameter[] commParam){
            //创建SqlCommand命令
            SqlCommand comm = new SqlCommand();
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //执行方法并传递参数
                PrepareCommand(conn, comm, commType, commText, commParam);
                //执行操作并接收结果
                object val = comm.ExecuteScalar();
                //返回结果
                return val;
            }
        }
 
        /// <summary>
        /// 执行SQL存储过程
        /// 注意：不能执行有输出（OUT）参数的存储过程
        /// </summary>
        /// <param name="connString">数据库连接</param>
        /// <param name="commName">存储过程名称</param>
        /// <param name="commParam">对象参数</param>
        /// <returns>执行结果对象</returns>
        public static object ExecuteScalar(string connString, string commName, params object[] commParam){
            //创建SqlCommand命令
            SqlCommand comm = new SqlCommand();
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //执行方法并传递参数
                PrepareCommand(conn, comm, commName, commParam);
                //执行操作并接收结果
                object val = comm.ExecuteScalar();
                //返回结果
                return val;
            }
        }
 
        /// <summary>
        /// 执行事务SQL语句
        /// 这个执行事务的好不好用忘记了
        /// </summary>
        /// <param name="connString">数据库连接</param>
        /// <param name="commParam">事务参数</param>
        public static void ExecuteTransaction(string connString, params string[] commParam){
            //使用Using释放SqlConnection资源
            using (SqlConnection conn = new SqlConnection(connString)){
                //打开数据库连接
                conn.Open();
                //开始事务
                SqlTransaction tran = conn.BeginTransaction();
                //创建SqlCommand命令
                SqlCommand comm = new SqlCommand();
                //设置SqlCommand的连接
                comm.Connection = conn;
                //设置SqlCommand的事务
                comm.Transaction = tran;
                try{
                    //循环执行事务
                    foreach (string param in commParam)
                    {
                        //设置SqlCommand的文本
                        comm.CommandText = param;
                        //执行事务
                        comm.ExecuteNonQuery();
                    }
                    //提交事务
                    tran.Commit();
                }catch{
                    //终止事务
                    tran.Rollback();
                    //抛出异常
                    throw;
                }finally{
                    //关闭数据库连接
                    conn.Close();
                }
            }
        }

     }
}