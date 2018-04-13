using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace YZ.Framework.SqlSugarDao
{
    /// <summary>
    /// SqlSugarClient实例
    /// </summary>
    public class SugarDao
    {
        public static SqlSugarClient GetInstance()
        {
            var config = LoadConfig();
            SqlSugarClient db = new SqlSugarClient(
                new ConnectionConfig()
                {
                    ConnectionString = config.Item1,
                    DbType = config.Item2,
                    IsAutoCloseConnection = true
                }
            );
            //db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    Console.WriteLine(sql + "\r\n" + db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            //    Console.WriteLine();
            //};
            return db;
        }

        private static Tuple<string, DbType> LoadConfig()
        {
            return new Tuple<string, DbType>("Data Source=.;Initial Catalog=hy;Persist Security Info=True;User ID=sa;Password=sa123!@#;", DbType.SqlServer);
        }
    }

}