using System.Data.Common;
using System.Data.SqlClient;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 定义创建SqlServer对象的实例，并转成基类型
    /// </summary>
    public class SqlServerClient : IDBClient
    {
        public DbConnection GetDbConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        public DbCommand GetDbCommand(string cmdText)
        {
            return new SqlCommand(cmdText);
        }

        public DbDataAdapter GetDbDataAdappter()
        {
            return new SqlDataAdapter();
        }

        public DbParameter GetDbParameter()
        {
            return new SqlParameter();
        }

        public string GetPagingSql(string cmdText, int pageIndex, int pageSize, string orderInfo)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            int endIndex = startIndex + pageSize + 1;
            cmdText = string.Format(@";with t1 as({0}),t2 as(select *,row_number() over ({1}) as rn from t1)
                                       select * from t2 where rn>{2} and rn<{3}", cmdText, orderInfo, startIndex, endIndex);
            return cmdText;
        }
    }
}

