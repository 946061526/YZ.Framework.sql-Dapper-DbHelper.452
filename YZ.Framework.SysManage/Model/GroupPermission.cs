namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 用户组权限
    /// </summary>
    public class GroupPermission
    {
        //groupID, menuID, operationCode
        /// <summary>
        /// 用户组编号
        /// </summary>
        public int groupID { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int menuID { get; set; }
        /// <summary>
        /// 菜单操作
        /// </summary>
        public string operationCode { get; set; }
    }
}
