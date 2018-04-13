using System;

namespace YZ.Framework.Bll
{
    /// <summary>
    /// 基本方法
    /// </summary>
    public interface IContext : IDisposable
    {
        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <param name="isIgnorPk">是否忽略主键</param>
        /// <returns>主键ID</returns>
        long Add<T>(T entity, bool isIgnorPk = true) where T : class, new();
        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <param name="isIgnorPk">是否忽略主键</param>
        /// <returns>本批次最大主键ID</returns>
        long Add<T>(T[] entitys, bool isIgnorPk = true) where T : class, new();
        /// <summary>
        /// 按主键批量删除(物理删除)
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="primaryKeys">主键集合</param>
        /// <returns>受影响行数</returns>
        int Delete<T>(int[] primaryKeys) where T : class, new();
        /// <summary>
        /// 按住键删除(物理删除)
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="primaryKey">主键值</param>
        /// <returns>受影响行数</returns>
        int Delete<T>(int primaryKey) where T : class, new();
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <returns>受影响行数</returns>
        int Edit<T>(T entity) where T : class, new();
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="id">主键</param>
        /// <returns>实体内容</returns>
        T Get<T>(int id) where T : class, new();
    }
}
