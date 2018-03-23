using System.Collections.Generic;

namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 当前登陆用户信息
    /// </summary>
    public class UserObject
    {
        /// <summary>
        /// 用户详细信息
        /// </summary>
        public UserInfo userInfo { get; set; }
        /// <summary>
        /// 所在部门
        /// </summary>
        public DeptInfo deptInfo { get; set; }
        /// <summary>
        /// 所在用户组
        /// </summary>
        public GroupInfo groupInfo { get; set; }
        /// <summary>
        /// 角色权限集合
        /// </summary>
        public List<GroupPermission> GroupPermissionList { get; set; }
        /// <summary>
        /// 角色菜单集合
        /// </summary>
        public List<MenuInfo> menuList { get; set; }
        /// <summary>
        /// 菜单操作集合
        /// </summary>
        public List<MenuOperation> MenuOperationList { get; set; }
    }
}
