using System.Data.Common;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 创建特定数据库实例，并转成基类型
    /// </summary>
    public interface IDBClient
    {
        DbConnection GetDbConnection(string connectionString);

        DbCommand GetDbCommand(string cmdText);

        DbDataAdapter GetDbDataAdappter();

        DbParameter GetDbParameter();

        string GetPagingSql(string cmdText, int pageIndex, int pageSize, string orderInfo);
    }
}
