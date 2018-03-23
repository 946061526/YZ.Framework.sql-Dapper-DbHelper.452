using System.Collections.Generic;

namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 用户组信息
    /// </summary>
    public class GroupInfo
    {
        //id, groupName, remark
        /// <summary>
        /// 用户组编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string groupName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        ///// <summary>
        ///// 用户组权限列表，包括菜单及菜单操作权限
        ///// </summary>
        //public List<GroupPermission> groupPermissionList { get; set; }
    }
}
