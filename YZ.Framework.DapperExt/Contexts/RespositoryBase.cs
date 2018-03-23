using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace YZ.Framework.DapperExt
{
    public class RespositoryBase<T> : IRespositoryBase<T> where T : class
    {
        readonly IDapperContext _context;
        //static readonly ProxyGenerator _generator = new ProxyGenerator();

        public IDapperContext Context => _context;

        public RespositoryBase(IDapperContext context)
        {
            _context = context;
            //#if DEBUG
            //          _context = _generator.CreateInterfaceProxyWithTarget(context, new SqlLogInterceptor(context.DBType));
            //#endif
        }

        #region 同步方法
        /// <inheritdoc />
        /// <summary>
        /// 根据表达式查询实体的数量
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> expression)
        {
            return _context.Count<T>(expression.ToPredicateGroup());
        }
        /// <inheritdoc />
        /// <summary>
        /// 根据主键获取一个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<TValue>(Expression<Func<T, object>> primaryKey, TValue value) where TValue : struct
        {
            return _context.Get<T, TValue>(ExpressionUtils.GetProperty(primaryKey), value);
        }
        /// <inheritdoc />
        /// <summary>
        /// 实体集合
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sorts"></param>
        /// <returns></returns>
        public IEnumerable<T> GetList(Expression<Func<T, bool>> expression, Sorting<T>[] sorts)
        {
            return _context.GetList<T>(expression.ToPredicateGroup(), sorts.ToSortable());
        }
        /// <inheritdoc />
        /// <summary>
        /// 分页集合
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="sorts"></param>
        /// <param name="page"></param>
        /// <param name="resultsPerPage"></param>
        /// <param name="isTotal"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        public IEnumerable<T> GetPage(Expression<Func<T, bool>> expression, Sorting<T>[] sorts, int page, int resultsPerPage, bool isTotal, ref int total)
        {
            if (sorts == null || !sorts.Any())
            {
                throw new ArgumentNullException(nameof(sorts));
            }
            var predicates = expression.ToPredicateGroup();
            if (isTotal)
            {
                total = _context.Count<T>(predicates);
            }
            return _context.GetPage<T>(predicates, sorts.ToSortable(), page, resultsPerPage);
        }
        public IEnumerable<T> QueryBySql(string sql, object param) => _context.QueryBySql<T>(sql, param);

        /// <inheritdoc />
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        public bool Insert(T entity, Expression<Func<T, object>> primaryKey = null) => _context.Insert(entity, ExpressionUtils.GetProperty(primaryKey)) > 0;
        /// <inheritdoc />
        /// <summary>
        /// 批量添加一个实体
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="primaryKey"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public bool Insert(IEnumerable<T> entities, Expression<Func<T, object>> primaryKey = null, int? commandTimeout = default(int?))
        {
            try
            {
                _context.Insert(entities, ExpressionUtils.GetProperty(primaryKey), commandTimeout);
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <inheritdoc />
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool Update(Expression<Func<T, bool>> expression, params DbFiled<T>[] fileds)
        {
            return _context.Update<T>(expression.ToPredicateGroup(), fileds.ToPropertyParam());
        }
        /// <inheritdoc />
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> expression)
        {
            return _context.Delete<T>(expression.ToPredicateGroup());
        }
        /// <inheritdoc />
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool DeleteLogic(Expression<Func<T, bool>> expression, params DbFiled<T>[] fileds)
        {
            return _context.DeleteLogic<T>(expression.ToPredicateGroup(), fileds.ToPropertyParam());
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="sqlList">sql语句集合</param>
        /// <returns></returns>
        public bool ExecTransaction(List<string> sqlList)
        {
            if (sqlList == null || !sqlList.Any())
                throw new ArgumentNullException("ExecTransaction：sqlList err");
            return _context.ExecTransaction(sqlList);
        }
        #endregion
    }
}
