using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Linq;
using DapperExtensions;

namespace YZ.Framework.DapperExt
{
    public class DapperContext : IDapperContext
    {
        /// <summary>
        /// sql生成
        /// </summary>
        readonly SqlBuilder _builder;
        /// <inheritdoc />
        /// <summary>
        /// dapper数据库连接实例
        /// </summary>
        public IDbConnection Db { get; }
        /// <inheritdoc />
        /// <summary>
        /// 数据库类型
        /// </summary>
        public string dbType { get; }

        public DapperContext(string appConfigKey, string dbType = DbType.MsSql)
        {
            Db = SqlFactory.GetDbInstance(appConfigKey, dbType);
            this.dbType = dbType;
            Db.Open();
            _builder = new SqlBuilder(SqlFactory.GetDapperConfiguration(dbType));
        }
        /// <inheritdoc />
        /// <summary>
        /// 获取一个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">主键</param>
        /// <param name="value">主键值</param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public T Get<T, T2>(string id, T2 value, int? commandTimeout = default(int?))
            where T : class
            where T2 : struct
        {
            using (Db)
            {
                var sql = _builder.SelectSingle<T>(id);
                var param = new DynamicParameters();
                param.Add(id, value);
                return Db.Query<T>(sql, param).SingleOrDefault();
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <param name="primaryKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Insert<T>(IEnumerable<T> entities, string primaryKey, int? commandTimeout = default(int?)) where T : class
        {
            var transaction = Db.BeginTransaction();
            var result = 0;
            try
            {
                var sql = _builder.Insert(entities.First(), primaryKey);
                result = Db.Execute(sql, entities, transaction, commandTimeout);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
            }
            finally
            {
                Db.Close();
            }
            return result;
        }
        /// <inheritdoc />
        /// <summary>
        /// 添加返回影响条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="primaryKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Insert<T>(T entity, string primaryKey, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                var sql = _builder.Insert(entity, primaryKey);
                return Db.Execute(sql, entity);
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="set"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Update<T>(IPredicate predicate, IDictionary<string, object> set, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                IDictionary<string, object> where = new Dictionary<string, object>();
                var sql = _builder.Update<T>(predicate, where, set.Select(x => x.Key));
                var param = new DynamicParameters();
                //where.AsList().ForEach(x => param.Add(x.Key, x.Value));
                //set.AsList().ForEach(x => param.Add(x.Key, x.Value));
                where.ToList().ForEach(x => param.Add(x.Key, x.Value));
                set.ToList().ForEach(x => param.Add(x.Key, x.Value));
                return Db.Execute(sql, param) > 0;
            }
        }
        public bool Delete<T>(IPredicate predicate, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                IDictionary<string, object> where = new Dictionary<string, object>();
                var sql = _builder.Delete<T>(predicate, where);
                var param = new DynamicParameters();
                where.ToList().ForEach(x => param.Add(x.Key, x.Value));
                //set.ToList().ForEach(x => param.Add(x.Key, x.Value));
                return Db.Execute(sql, param) > 0;
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="set"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool DeleteLogic<T>(IPredicate predicate, IDictionary<string, object> set, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                IDictionary<string, object> where = new Dictionary<string, object>();
                var sql = _builder.Update<T>(predicate, where, set.Select(x => x.Key));
                var param = new DynamicParameters();
                where.ToList().ForEach(x => param.Add(x.Key, x.Value));
                set.ToList().ForEach(x => param.Add(x.Key, x.Value));
                return Db.Execute(sql, param) > 0;
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 实体数量
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public int Count<T>(IPredicate predicate, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                return Db.Count<T>(predicate);
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 获取实体集合(分页)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPage<T>(IPredicate predicate, IList<ISort> sort, int page, int resultsPerPage, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                if (page < 1)
                {
                    page = 1;
                }
                page -= 1;
                var sql = _builder.GetPaging(typeof(T), predicate, sort, page, resultsPerPage, out var parameters);
                return Db.Query<T>(sql, parameters);
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList<T>(IPredicate predicate, IList<ISort> sort, int? commandTimeout = default(int?)) where T : class
        {
            using (Db)
            {
                var sql = _builder.GetList(typeof(T), predicate, sort, out var parameters);
                //return Db.GetList<T>(predicate, sort);
                return Db.Query<T>(sql, parameters);
            }
        }
        /// <inheritdoc />
        /// <summary>
        /// sql查询
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public IEnumerable<T> QueryBySql<T>(string sql, object param) where T : class
        {
            using (Db)
            {
                return Db.Query<T>(sql, param);
            }
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlList">sql语句集合</param>
        /// <returns></returns>
        public bool ExecTransaction(List<string> sqlList)
        {
            using (Db)
            {
                var transaction = Db.BeginTransaction();
                try
                {
                    foreach (var sql in sqlList)
                    {
                        Db.Execute(sql, null, transaction, null);
                    }
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
