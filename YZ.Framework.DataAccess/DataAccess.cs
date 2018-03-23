using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 访问数据库，并聚合IDBClient来创建特定数据库对象的实例
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// 连接串
        /// </summary>
        private static string ConnectionString;
        /// <summary>
        /// 数据库驱动
        /// </summary>
        private static IDBClient DBClient;

        static DataAccess()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["dbDefault"].ConnectionString;
            DBClient = DBClientDriver.GetDBClient(ConfigurationManager.AppSettings["dbClient"]);
        }

        #region Connection
        /// <summary>
        /// 打开链接
        /// </summary>
        /// <param name="conn"></param>
        private static void OpenConnection(DbConnection conn)
        {
            if (conn == null) conn = DBClient.GetDbConnection(ConnectionString);
            if (conn.State == ConnectionState.Closed) conn.Open();
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="conn"></param>
        private static void CloseConnection(DbConnection conn)
        {
            if (conn == null) return;
            if (conn.State == ConnectionState.Open) conn.Close();
            conn.Dispose();
            conn = null;
        }
        #endregion

        #region ExecuteNonQuery

        /// <summary>
        /// 执行sql语句，返回受影响行数
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>int</returns>
        public static int ExecuteNonQueryBySql(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteNonQuery(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 执行存储过程，返回受影响行数
        /// </summary>
        /// <param name="cmdText">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>int</returns>
        public static int ExecuteNonQueryByProc(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, procName, parameterValues);
        }

        /// <summary>
        /// 执行sql语句或存储过程，返回受影响行数
        /// </summary>
        private static int ExecuteNonQuery(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            int result = 0;
            bool mustCloseConn = true;

            DbCommand cmd = PrepareCmd(cmdType, cmdText, parameterValues, out mustCloseConn);
            OpenConnection(cmd.Connection);
            result = cmd.ExecuteNonQuery();

            if (mustCloseConn) CloseConnection(cmd.Connection);
            ClearCmdParameters(cmd);
            cmd.Dispose();

            return result;
        }

        #endregion ExecuteNonQuery

        #region ExecuteReader

        /// <summary>
        /// 返回DbDataReader，不建议用此方法
        /// </summary>
        public static DbDataReader ExecuteReaderBySql(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteReader(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 返回DbDataReader，不建议用此方法
        /// </summary>
        public static DbDataReader ExecuteReaderByProc(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteReader(CommandType.StoredProcedure, procName, parameterValues);
        }

        /// <summary>
        /// 返回DbDataReader，不建议用此方法
        /// </summary>
        private static DbDataReader ExecuteReader(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            DbDataReader result = null;
            bool mustCloseConn = true;
            DbCommand cmd = PrepareCmd(cmdType, cmdText, parameterValues, out mustCloseConn);
            try
            {
                OpenConnection(cmd.Connection);
                if (mustCloseConn)
                    result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    result = cmd.ExecuteReader();

                ClearCmdParameters(cmd);
                return result;
            }
            catch
            {
                if (mustCloseConn) CloseConnection(cmd.Connection);
                ClearCmdParameters(cmd);
                cmd.Dispose();
                throw new Exception("ExecuteReader error");
            }
        }

        #endregion ExecuteReader

        #region ExecuteDataset or ExecuteDataTable

        /// <summary>
        /// 执行sql语句，返回DataSet
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSetBySql(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteDataSet(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 执行存储过程，返回DataSet
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecuteDataSetByProc(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteDataSet(CommandType.StoredProcedure, procName, parameterValues);
        }

        /// <summary>
        /// 返回DataSet
        /// </summary>
        private static DataSet ExecuteDataSet(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            DataSet result = null;
            bool mustCloseConn = true;

            DbCommand cmd = PrepareCmd(cmdType, cmdText, parameterValues, out mustCloseConn);
            using (DbDataAdapter da = DBClient.GetDbDataAdappter())
            {
                da.SelectCommand = cmd;
                result = new DataSet();
                da.Fill(result);
            }

            if (mustCloseConn) CloseConnection(cmd.Connection);
            ClearCmdParameters(cmd);
            cmd.Dispose();

            return result;
        }

        /// <summary>
        /// 执行sql语句，返回DataTable
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>DataSet</returns>
        public static DataTable ExecuteDataTableBySql(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteDataTable(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 执行存储过程，返回DataTable
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>DataSet</returns>
        public static DataTable ExecuteDataTableByProc(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteDataTable(CommandType.StoredProcedure, procName, parameterValues);
        }
        /// <summary>
        /// 返回DataTable
        /// </summary>
        private static DataTable ExecuteDataTable(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            DataSet ds = ExecuteDataSet(cmdType, cmdText, parameterValues);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }
        #endregion

        #region ExecutePaging
        //为了支持数据分页，添加了两个方法，分别返回DataTable和DbDataReader, DbDataReader用于在ORM中转成实体对象。

        public static DataTable ExecutePagingDataTable(CommandType cmdType, string cmdText, int pageIndex, int pageSize, string orderBy, params DbParameter[] parameterValues)
        {
            cmdText = DBClient.GetPagingSql(cmdText, pageIndex, pageSize, orderBy);
            return ExecuteDataTable(CommandType.Text, cmdText, parameterValues);
        }

        public static DbDataReader ExecutePagingReader(CommandType cmdType, string cmdText, int pageIndex, int pageSize, string orderBy, params DbParameter[] parameterValues)
        {
            cmdText = DBClient.GetPagingSql(cmdText, pageIndex, pageSize, orderBy);
            return ExecuteReader(CommandType.Text, cmdText, parameterValues);
        }
        #endregion

        #region ExecuteScalar
        /// <summary>
        /// 执行sql语句，返回对象
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>object</returns>
        public static object ExecuteScalarBySql(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteScalar(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 执行存储过程，返回对象
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>object</returns>
        public static object ExecuteScalarByProc(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteScalar(CommandType.StoredProcedure, procName, parameterValues);
        }
        private static object ExecuteScalar(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            object result = 0;
            bool mustCloseConn = true;

            DbCommand cmd = PrepareCmd(cmdType, cmdText, parameterValues, out mustCloseConn);
            OpenConnection(cmd.Connection);
            result = cmd.ExecuteScalar();

            if (mustCloseConn) CloseConnection(cmd.Connection);
            ClearCmdParameters(cmd);
            cmd.Dispose();

            return result;
        }

        /// <summary>
        /// 执行sql语句，返回泛型
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>object</returns>
        public static T ExecuteScalarBySql<T>(string sql, params DbParameter[] parameterValues)
        {
            return ExecuteScalar<T>(CommandType.Text, sql, parameterValues);
        }
        /// <summary>
        /// 执行存储过程，返回泛型
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameterValues">参数</param>
        /// <returns>object</returns>
        public static T ExecuteScalarByProc<T>(string procName, params DbParameter[] parameterValues)
        {
            return ExecuteScalar<T>(CommandType.StoredProcedure, procName, parameterValues);
        }
        private static T ExecuteScalar<T>(CommandType cmdType, string cmdText, params DbParameter[] parameterValues)
        {
            T result = default(T);
            bool mustCloseConn = true;
            DbCommand cmd = PrepareCmd(cmdType, cmdText, parameterValues, out mustCloseConn);
            OpenConnection(cmd.Connection);
            object obj = cmd.ExecuteScalar();
            result = (T)Convert.ChangeType(obj, typeof(T));

            if (mustCloseConn) CloseConnection(cmd.Connection);
            ClearCmdParameters(cmd);
            cmd.Dispose();

            return result;
        }
        #endregion ExecuteScalar

        #region GetList

        /// <summary>
        /// 根据sql语句，获取结果集
        /// </summary>
        public static List<T> GetList<T>(string sql, params DbParameter[] parameterValues) where T : new()
        {
            List<T> list = new List<T>();
            DbDataReader dr = ExecuteReaderBySql(sql, parameterValues);
            while (dr.Read())
            {
                T obj = GetEntity<T>(dr);
                list.Add(obj);
            }
            return list;
        }
        /// <summary>
        /// 根据sql语句，分页获取结果集
        /// </summary>
        public static List<T> GetList<T>(string sql, int pageIndex, int pageSize, string orderBy, params DbParameter[] parameterValues) where T : new()
        {
            List<T> list = new List<T>();
            sql = DBClient.GetPagingSql(sql, pageIndex, pageSize, orderBy);
            DbDataReader dr = ExecuteReaderBySql(sql, parameterValues);
            while (dr.Read())
            {
                T obj = GetEntity<T>(dr);
                list.Add(obj);
            }
            return list;
        }

        private static T GetEntity<T>(DbDataReader dr) where T : new()
        {
            T entity = new T();
            foreach (var pi in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var obj = new object();
                try
                {
                    obj = dr[pi.Name];
                }
                catch
                {
                    continue;
                }

                if (obj == DBNull.Value || obj == null) continue;
                var si = pi.GetSetMethod();
                if (si == null) continue;
                pi.SetValue(entity, obj, null);
            }
            return entity;
        }

        #endregion

        #region Transaction

        /// <summary>
        /// 维护一个TransConnection类型的属性，并添加ThreadStatic，可以维护在线程级别上的唯一性
        /// </summary>
        [ThreadStatic]
        private static TransConnection TransConnectionObj = null;

        //当为嵌套事务时，首次调用BeginTransaction，同样会创建新的TransConnection对象，深度默认为0，并保存在TransConnectionObj字段上；
        //第n(n>1)次调用时方法时，仅会累加嵌套的深度，不会开起新的事务。

        public static void BeginTransaction()
        {
            if (TransConnectionObj == null)
            {
                DbConnection conn = DBClient.GetDbConnection(ConnectionString);
                OpenConnection(conn);
                DbTransaction trans = conn.BeginTransaction();
                TransConnectionObj = new TransConnection();
                TransConnectionObj.DBTransaction = trans;
            }
            else
                TransConnectionObj.Deeps += 1;
        }

        //当CommitTransaction提交事务时，
        //如果深度Deeps>0，那么表示此次提交的事务是内层事务，计数器减1即可；
        //如果深度Deeps=0时，表示为最外层事务，刚做实际上的提交事务工作；
        public static void CommitTransaction()
        {
            if (TransConnectionObj == null) return;
            if (TransConnectionObj.Deeps > 0)
                TransConnectionObj.Deeps -= 1;
            else
            {
                TransConnectionObj.DBTransaction.Commit();
                ReleaseTransaction();
            }
        }

        //当RollbackTransaction提交事务时，
        //如果深度Deeps>0，那么表示此次提交的事务是内层事务，计数器减1即可;
        //如果深度Deeps=0时，表示为最外层事务，刚做实际上的回滚操作;
        public static void RollbackTransaction()
        {
            if (TransConnectionObj == null) return;
            if (TransConnectionObj.Deeps > 0)
                TransConnectionObj.Deeps -= 1;
            else
            {
                TransConnectionObj.DBTransaction.Rollback();
                ReleaseTransaction();
            }
        }
        /// <summary>
        /// 事务释放
        /// </summary>
        private static void ReleaseTransaction()
        {
            if (TransConnectionObj == null) return;
            DbConnection conn = TransConnectionObj.DBTransaction.Connection;
            TransConnectionObj.DBTransaction.Dispose();
            TransConnectionObj = null;
            CloseConnection(conn);
        }

        #endregion

        #region Create DbParameter

        /// <summary>
        /// 创建参数，默认string类型
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbParameter CreateParametersWithValue(string name, object value)
        {
            return CreateParameter(name, value);
        }
        /// <summary>
        /// 创建输入型参数
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <param name="size">参数大小</param>
        /// <returns>DbParameter</returns>
        public static DbParameter CreateParameterInput(string paraName, object value, DbType type, int size = 0)
        {
            return CreateParameter(paraName, value, type, size, ParameterDirection.Input);
        }
        /// <summary>
        /// 创建输出型参数
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns>DbParameter</returns>
        public static DbParameter CreateParameterOutput(string paraName, DbType type, int size = 0)
        {
            return CreateParameter(paraName, null, type, size, ParameterDirection.Output);
        }
        /// <summary>
        /// 创建返回值型参数
        /// </summary>
        /// <param name="paraName">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <returns>DbParameter</returns>
        public static DbParameter CreateParameterReturnValue(string paraName, DbType type, int size = 0)
        {
            return CreateParameter(paraName, null, type, size, ParameterDirection.ReturnValue);
        }
        /// <param name="paraName">参数名</param>
        /// <param name="type">参数类型</param>
        /// <param name="value">参数值</param>
        /// <param name="size">参数大小</param>
        /// <param name="direction">类型(输入 / 输出 / 返回值)</param>
        /// <returns>DbParameter</returns>
        public static DbParameter CreateParameter(string paraName, object value, DbType type = DbType.String, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            DbParameter para = DBClient.GetDbParameter();
            para.ParameterName = paraName;

            if (size != 0) para.Size = size;

            para.DbType = type;
            para.Value = value ?? DBNull.Value;
            para.Direction = direction;

            return para;
        }

        #endregion

        #region Command and Parameter (private methods)
        /// <summary>
        /// 预处理用户提供的命令,数据库连接/事务/命令类型/参数
        /// </summary>
        /// <param>要处理的DbCommand</param>
        /// <param>数据库连接</param>
        /// <param>一个有效的事务或者是null值</param>
        /// <param>命令类型 (存储过程,命令文本, 其它.)</param>
        /// <param>存储过程名或都T-SQL命令文本</param>
        /// <param>和命令相关联的DbParameter参数数组,如果没有参数为'null'</param>
        /// <param><c>true</c> 如果连接是打开的,则为true,其它情况下为false.</param>
        private static DbCommand PrepareCmd(CommandType cmdType, string cmdText, DbParameter[] cmdParams, out bool mustCloseConn)
        {
            DbCommand cmd = DBClient.GetDbCommand(cmdText);

            DbConnection conn = null;
            if (TransConnectionObj != null)
            {
                conn = TransConnectionObj.DBTransaction.Connection;
                cmd.Transaction = TransConnectionObj.DBTransaction;
                mustCloseConn = false;
            }
            else
            {
                conn = DBClient.GetDbConnection(ConnectionString);
                mustCloseConn = true;
            }
            cmd.Connection = conn;
            cmd.CommandType = cmdType;
            AttachParameters(cmd, cmdParams);

            return cmd;
        }

        /// <summary>
        /// 将DbParameter参数数组(参数值)分配给DbCommand命令.
        /// 这个方法将给任何一个参数分配DBNull.Value;
        /// 该操作将阻止默认值的使用.
        /// </summary>
        /// <param>命令名</param>
        /// <param>SqlParameters数组</param>
        private static void AttachParameters(DbCommand command, DbParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (DbParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
                        if ((p.Direction == ParameterDirection.InputOutput || p.Direction == ParameterDirection.Input) && (p.Value == null))
                            p.Value = DBNull.Value;

                        command.Parameters.Add(p);
                    }
                }
            }
        }
        /// <summary>
        /// 清除参数
        /// </summary>
        /// <param name="cmd"></param>
        private static void ClearCmdParameters(DbCommand cmd)
        {
            bool canClear = true;
            if (cmd.Connection != null && cmd.Connection.State != ConnectionState.Open)
            {
                foreach (DbParameter commandParameter in cmd.Parameters)
                {
                    if (commandParameter.Direction != ParameterDirection.Input)
                    {
                        canClear = false;
                        break;
                    }
                }
            }
            if (canClear) cmd.Parameters.Clear();
        }
        #endregion
    }
}
