using DapperExtensions.Sql;
using DapperExtensions.Mapper;
using System.Reflection;
using System.Collections.Generic;
using System.Data;
using DapperExtensions;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace YZ.Framework.DapperExt
{
    internal class SqlFactory
    {
        /// <summary>
        /// dapper连接实例
        /// </summary>
        /// <param name="confKey">webconfig配置键</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        public static IDbConnection GetDbInstance(string confKey, string dbType = DbType.MsSql)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[confKey].ToString();
            switch (dbType)
            {
                case DbType.MySql:
                    DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
                    return new MySqlConnection(connectionString);
                case DbType.MsSql:
                    DapperExtensions.DapperExtensions.SqlDialect = new SqlServerDialect();
                    return new SqlConnection(connectionString);
            }
            return null;
        }

        /// <summary>
        /// SqlGenerator实例
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static ISqlGenerator GetSqlGenerator(IDapperExtensionsConfiguration configuration)
        {
            return new SqlGeneratorImpl(configuration);
        }

        /// <summary>
        /// DapperExtensions配置实例
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static IDapperExtensionsConfiguration GetDapperConfiguration(string dbType = DbType.MsSql)
        {
            switch (dbType)
            {
                case DbType.MySql:
                    return new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new MySqlDialect());
                case DbType.MsSql:
                    return new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new SqlServerDialect());
            }
            return null;
        }
    }
}
