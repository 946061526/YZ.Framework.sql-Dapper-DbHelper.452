using System.Collections.Generic;

namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class MenuInfo
    {
        //id, cnName, enName, pid, sort, dllName, reflectType, state, remark
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string cnName { get; set; }
        /// <summary>
        /// 菜单标识
        /// </summary>
        public string enName { get; set; }
        /// <summary>
        /// 上级菜单编号
        /// </summary>
        public int pid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }
        /// <summary>
        /// dll名称
        /// </summary>
        public string dllName { get; set; }
        /// <summary>
        /// 反射名称
        /// </summary>
        public string reflectType { get; set; }
        /// <summary>
        /// 状态(1.正常 2.不可见 3.删除)
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        ///// <summary>
        ///// 菜单操作项集合
        ///// </summary>
        //public List<MenuOperation> menuOperationList { get; set; }
    }
}
