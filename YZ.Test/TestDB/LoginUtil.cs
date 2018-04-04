using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZ.Framework.SysManage.Model;

namespace YZ.Test.TestDB
{
    public class LoginUtil
    {
        UserObject UserObj = new UserObject();

        //public DeptInfo GetDeptInfo(int DeptID)
        //{
        //    var g = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<DeptInfo>>().Get(x => x.id, DeptID);
        //    return g;
        //}
        public static async Task<DeptInfo> GetDeptInfo(int DeptID)
        {
            //var g = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<DeptInfo>>().Get(x => x.id, DeptID);
            //return g;

            return await Task.Run(() =>
            {
                var g = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<DeptInfo>>().Get(x => x.Id, DeptID);
                return g;
            });
        }
        public GroupInfo GetGroupInfo(int GroupID)
        {
            var g = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<GroupInfo>>().Get(x => x.Id, GroupID);
            return g;
        }

        public List<GroupPermission> GetGroupPermission(int GroupID)
        {
            var g = Framework.DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<GroupPermission>>().GetList(x => x.GroupID == GroupID, null);
            return g as List<GroupPermission>;
        }

        public List<MenuInfo> GetMenuInfo()
        {
            string mids = string.Join(",", UserObj.GroupPermissionList.Select(o => o.MenuID).Distinct());
            string sql = string.Format(@"
WITH menus([id],[cnName],[enName],[pid],[sort],[dllName],[reflectType],[state],[remark])
AS
(
    SELECT * FROM [MenuInfo] A where a.id in({0})
UNION ALL
	SELECT a.* FROM [MenuInfo] a INNER JOIN menus p ON p.pid=a.id
)
SELECT distinct * FROM menus order by id", mids);
            var m = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<MenuInfo>>().QueryBySql(sql, null);
            UserObj.MenuList = m as List<MenuInfo>;
            return UserObj.MenuList;
        }

        public List<MenuOperation> GetMenuOperation()
        {
            var m = IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<MenuOperation>>().GetList(null, null);
            UserObj.MenuOperationList = m as List<MenuOperation>;
            return UserObj.MenuOperationList;
        }
    }
}
