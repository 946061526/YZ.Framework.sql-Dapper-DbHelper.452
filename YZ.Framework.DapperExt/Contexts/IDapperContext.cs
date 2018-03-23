using DapperExtensions;
using System.Collections.Generic;
using System.Data;

namespace YZ.Framework.DapperExt
{
    public interface IDapperContext
    {
        /// <summary>
        /// DbConnection
        /// </summary>
        IDbConnection Db { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        string dbType { get; }

        /// <summary>
        /// 添加一组实体(pk为空时,需要给指定主键赋值;pk不为空,则排除该主键)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="primaryKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>返回影响条数</returns>
        int Insert<T>(IEnumerable<T> entities, string primaryKey, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 添加一个实体(pk为空时,需要给指定主键赋值;pk不为空,则排除该主键)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="primaryKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>返回影响条数</returns>
        int Insert<T>(T entity, string primaryKey, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 更新语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">where条件</param>
        /// <param name="set">更新字段</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Update<T>(IPredicate predicate, IDictionary<string, object> set, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 删除语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">where条件</param>
        /// <param name="set">更新字段</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete<T>(IPredicate predicate, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">where条件</param>
        /// <param name="set">更新字段</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool DeleteLogic<T>(IPredicate predicate, IDictionary<string, object> set, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 实体的数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">where条件</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        int Count<T>(IPredicate predicate, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 获取实体集合(分页)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">where条件</param>
        /// <param name="sort">排序</param>
        /// <param name="page">页</param>
        /// <param name="resultsPerPage">每页显示条数</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        IEnumerable<T> GetList<T>(IPredicate predicate, IList<ISort> sort, int? commandTimeout = default(int?)) where T : class;
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">主键</param>
        /// <param name="value">主键值</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T Get<T, T2>(string id, T2 value, int? commandTimeout = default(int?))
            where T : class
            where T2 : struct;
        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        IEnumerable<T> QueryBySql<T>(string sql, object param) where T : class;
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlList">sql语句集合</param>
        /// <returns></returns>
        bool ExecTransaction(List<string> sqlList);
    }
}