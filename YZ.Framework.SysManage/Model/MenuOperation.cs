namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 菜单及操作项
    /// </summary>
    public class MenuOperation
    {
        //menuID, operationCode
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int menuID { get; set; }
        /// <summary>
        /// 操作项编码(如：Add,Delete,Update,Query....)
        /// </summary>
        public string operationCode { get; set; }
    }
}
