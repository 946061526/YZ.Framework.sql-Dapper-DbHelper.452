using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using YZ.Framework.DapperExt;
using YZ.Framework.SysManage.Model;

namespace YZ.Framework.Bll
{
    public class BllDept
    {
        public BllDept()
        {
        }

        public static BllDept Instance = new BllDept();

        public bool Insert(DeptInfo entity)
        {
            return IocContainer.Resolve<IRespositoryBase<DeptInfo>>().Insert(entity, t => t.Id);
        }

        public bool Delete(int id)
        {
            return IocContainer.Resolve<IRespositoryBase<DeptInfo>>().Delete(t => t.Id == id);
        }

        //public bool DeleteLogic(int id)
        //{
        //    DbFiled<DeptInfo>[] fs = {
        //        new DbFiled<DeptInfo>(x => x.Id,0)
        //    };
        //    return _RespositoryBase.DeleteLogic(t => t.Id == id, fs);
        //}

        public bool Update(DeptInfo entity)
        {
            DbFiled<DeptInfo>[] fs = {
                new DbFiled<DeptInfo>(x => x.DeptName, entity.DeptName),
                new DbFiled<DeptInfo>(x => x.Remark, entity.Remark)
            };
            return IocContainer.Resolve<IRespositoryBase<DeptInfo>>().Update(x => x.Id == entity.Id, fs);
        }

        public DeptInfo Get(int id)
        {
            return IocContainer.Resolve<IRespositoryBase<DeptInfo>>().Get(t => t.Id == id);
        }

        public List<DeptInfo> GetList()
        {
            return IocContainer.Resolve<IRespositoryBase<DeptInfo>>().GetList(null, null) as List<DeptInfo>;
        }

        public List<DeptInfo> GetList(int pageIndex, int pageSize, ref int total)
        {
            Sorting<DeptInfo>[] sorts = { new Sorting<DeptInfo>(x => x.Id, SortType.Desc) };
            return IocContainer.Instance.Resolve<IRespositoryBase<DeptInfo>>().GetPage(x => x.Id > 0, sorts, pageIndex, pageSize, true, ref total) as List<DeptInfo>;
        }
    }
}
