using MySql.Data.MySqlClient;
using System.Data.Common;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 定义创建MySql对象的实例，并转成基类型
    /// </summary>
    public class MySqlClient : IDBClient
    {
        public DbConnection GetDbConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        public DbCommand GetDbCommand(string cmdText)
        {
            return new MySqlCommand(cmdText);
        }

        public DbDataAdapter GetDbDataAdappter()
        {
            return new MySqlDataAdapter();
        }

        public DbParameter GetDbParameter()
        {
            return new MySqlParameter();
        }

        public string GetPagingSql(string cmdText, int pageIndex, int pageSize, string orderInfo)
        {
            int startIndex = (pageIndex - 1) * pageSize;
            cmdText = string.Format(@"{0} {1} limit {2}, {3}", cmdText, orderInfo, startIndex, pageSize);
            return cmdText;
        }
    }
}
