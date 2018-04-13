﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Framework.SysManage.Model;

namespace YZ.Framework.Bll
{
    public interface ITestGroup : IDisposable
    {
        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <param name="ignorePk">是否忽略主键</param>
        /// <returns>主键ID</returns>
        long Add(GroupInfo entity, bool ignorePk = true);
        /// <summary>
        /// 插入一个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <param name="ignorePk">是否忽略主键</param>
        /// <returns>本批次最大主键ID</returns>
        long Add(List<GroupInfo> entitys, bool ignorePk = true);
        /// <summary>
        /// 按主键批量删除(物理删除)
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="primaryKeys">主键集合</param>
        /// <param name="isLogic">是否逻辑删除，默认是</param>
        /// <returns>受影响行数</returns>
        int Delete(int[] primaryKeys, bool isLogic);
        /// <summary>
        /// 按住键删除(物理删除)
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="primaryKey">主键值</param>
        /// <param name="isLogic">是否逻辑删除，默认是</param>
        /// <returns>受影响行数</returns>
        int Delete(int primaryKey, bool isLogic);
        /// <summary>
        /// 更新实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="entity">实体内容</param>
        /// <returns>受影响行数</returns>
        int Edit(GroupInfo entity);
        /// <summary>
        /// 查询单个实体
        /// </summary>
        /// <typeparam name="T">表实体</typeparam>
        /// <param name="id">主键</param>
        /// <returns>实体内容</returns>
        GroupInfo Get(int id);
        List<GroupInfo> GetList(int id, string name, int pageIndex, int pageSize, string orderFileds, ref int total);
    }
}
