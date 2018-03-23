using System.Reflection;

namespace YZ.Framework.DataAccess
{
    /// <summary>
    /// 根据类名动态创建IDBClient的实现类
    /// </summary>
    public class DBClientDriver
    {
        private static readonly string path = "YZ.Framework.DataAccess";
        /// <summary>
        /// 通过反射实例化IDBClient
        /// </summary>
        /// <param name="dbClientClassName">反射类名，默认SqlServerClient</param>
        /// <returns>IDBClient</returns>
        public static IDBClient GetDBClient(string dbClientClassName = "SqlServerClient")
        {
            string className = string.Format("{0}.{1}", path, dbClientClassName);
            return (IDBClient)Assembly.Load(path).CreateInstance(className);
        }
    }
}