namespace YZ.Framework.SysManage.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        //id, cnName, enName, deptID, pwd, sex, age, qq, mobile, email, state, remark
        /// <summary>
        /// 用户编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string cnName { get; set; }
        /// <summary>
        /// 英文名/登录名
        /// </summary>
        public string enName { get; set; }
        /// <summary>
        /// 所属部门编号
        /// </summary>
        public int deptID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int age { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string qq { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public string mobile { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 状态(1.正常 2.禁用 3.删除)
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
