using System;
using System.Collections.Generic;
using System.Data;

namespace YZ.Framework.Bll
{
    /// <summary>
    /// Ado操作
    /// </summary>
    public interface IContextAdo : IDisposable
    {
        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="TReturn">查询返回实体</typeparam>
        /// <param name="sql">sql字符串</param>
        /// <param name="dic">字典</param>
        /// <returns>返回实体</returns>
        IEnumerable<TReturn> Query<TReturn>(string sql, IDictionary<string, object> dic = null) where TReturn : class, new();
        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        TReturn QuerySingle<TReturn>(string sql, IDictionary<string, object> dic = null) where TReturn : class, new();
        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <param name="dic">字典</param>
        /// <returns>返回datatable</returns>
        DataTable GetDataTable(string sql, IDictionary<string, object> dic = null);
        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <param name="dic">字典</param>
        /// <returns>返回dataset</returns>
        DataSet GetDataSet(string sql, IDictionary<string, object> dic = null);

        /// <summary>
        /// 执行sql增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <param name="isTran"></param>
        /// <returns>int</returns>
        int ExecuteSql(string sql, IDictionary<string, object> dic = null, bool isTran = false);

        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <returns>int</returns>
        int GetInt(string sql, IDictionary<string, object> dic = null);
        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <returns>string</returns>
        string GetString(string sql, IDictionary<string, object> dic = null);
        /// <summary>
        /// 获取首行首列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dic"></param>
        /// <returns>Decimal</returns>
        string GetDecimal(string sql, IDictionary<string, object> dic = null);
    }
}
