using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YZ.Framework.SysManage.Model;

namespace YZ.Framework
{
    /// <summary>
    /// 登陆辅助类
    /// </summary>
    public class LoginUtil
    {
        /// <summary>
        /// 用事件处理
        /// Cursor: 0.表示出错,程序不能正常运行 1.表示没有配置网络连接
        /// </summary>
        /// <param name="Cursor"></param>
        /// <param name="Messages">提示消息</param>
        public delegate void LoginEventArgs(int Cursor, string Messages);
        public event LoginEventArgs LoginEventHandler;

        public Core.ISystemApplication SystemApplicaion { get; set; }

        public String UserName { get; set; }

        public String UserPass { get; set; }

        //用户信息
        private UserInfo UserInfo = new UserInfo();
        /// <summary>
        /// 用户信息对象
        /// </summary>
        private UserObject UserObject = new UserObject();

        /// <summary>
        /// 执行登陆
        /// </summary>
        /// <returns></returns>
        public bool Login()
        {
            try
            {
                DateTime dt1 = DateTime.Now;
                Program.MainApp = new MainApp();

                LoginMeaus(8, "正在初始化数据...");
                SystemConfig sysConfig = InitSystemConfig();

                LoginMeaus(8, "连接数据库...");

                LoginMeaus(8, "验证用户信息...");
                if (!CheckUser())
                    return false;

                LoginMeaus(8, "加载基础数据....");
                GetUserData();
                                
                SystemApplicaion.UserObject = UserObject;

                LoginMeaus(8, "加载主界面....");
                //Program.MainApp.Name = sysConfig.SystemName;

                Program.MainApp.WindowState = FormWindowState.Maximized;
                DateTime dt2 = DateTime.Now;
                string temp = (dt2 - dt1).ToString();
                return true;
            }
            catch
            {
                LoginMeaus(1, "连接服务器错误！");
                return false;
            }
        }

        /// <summary>
        /// 读取系统配置
        /// </summary>
        /// <returns></returns>
        private SystemConfig InitSystemConfig()
        {
            SystemConfig config = new SystemConfig();
            return config;
        }

        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <returns></returns>
        private bool CheckUser()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                LoginMeaus(0, "用户名不能为空");
                return false;
            }
            if (string.IsNullOrEmpty(UserPass))
            {
                LoginMeaus(0, "密码不能为空");
                return false;
            }
            UserPass = Utility.Encrypt.MD5String(UserPass);
            //1.用户信息
            UserInfo = GetUserInfo(UserName, UserPass);
            if (UserInfo == null)
            {
                LoginMeaus(0, "用户名或密码不正确,登录失败");
                return false;
            }
            if (UserInfo.State == 2)
            {
                LoginMeaus(0, "此用户名已被禁用,登录失败");
                return false;
            }
            UserObject.UserInfo = UserInfo;
            return true;
        }


        #region 获取用户数据

        /// <summary>
        /// 获取用户信息
        /// </summary>
        private UserInfo GetUserInfo(string UserName, string Pwd)
        {
            var u = DapperExt.IocContainer.Instance.Resolve<DapperExt.IRespositoryBase<UserInfo>>()
                .Get(x => x.EnName == UserName && x.Pwd == Pwd);
            return u;
        }

        /// <summary>
        /// 加载数据(异步)
        /// </summary>
        private void GetUserData()
        {
            ////新建一个Task
            //Task t1 = new Task(() =>
            //{
            //    GetGroupInfo();
            //});

            Task pTask = new Task(() =>
            {
                CancellationTokenSource cts = new CancellationTokenSource(5000);
                //创建任务工厂
                TaskFactory tf = new TaskFactory(cts.Token, TaskCreationOptions.AttachedToParent, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
                //添加一组具有相同状态的子任务
                Task[] task = new Task[]{
                    tf.StartNew(() => { GetGroupPermission(); }),
                    tf.StartNew(() => { GetDept(); }),
                    tf.StartNew(() => { GetUserGroup(); })
                };
            });
            pTask.ContinueWith(t => { GetMenuInfo(); });
            pTask.Start();
            Thread.Sleep(500);
        }

        ///// <summary>
        ///// 获取用户所在组信息
        ///// </summary>
        //private GroupInfo GetGroupInfo()
        //{
        //    var g = DapperExt.IocContainer.Instance.Resolve<DapperExt.IRespositoryBase<GroupInfo>>()
        //        .Get(x => x.Id, UserInfo.GroupID);

        //    return UserObject.GroupInfo = g;
        //}

        /// <summary>
        /// 获取用户组操作权限
        /// </summary>
        /// <param name="groupID"></param>
        /// <returns></returns>
        private List<GroupPermission> GetGroupPermission()
        {
            var list = DapperExt.IocContainer.Instance.Resolve<DapperExt.IRespositoryBase<GroupPermission>>()
                .GetList(x => x.GroupID == UserInfo.GroupID, null) as List<GroupPermission>;

            return UserObject.GroupPermissionList = list;
        }

        /// <summary>
        /// 获取用户组菜单
        /// </summary>
        /// <returns></returns>
        private List<MenuInfo> GetMenuInfo()
        {
            string mids = string.Join(",", UserObject.GroupPermissionList.Select(o => o.MenuID).Distinct());
            string sql = string.Format(@"
WITH menus([id],[cnName],[enName],[pid],[sort],[dllName],[reflectType],[state],[remark])
AS
(
    SELECT * FROM [MenuInfo] A where a.id in({0})
UNION ALL
	SELECT a.* FROM [MenuInfo] a INNER JOIN menus p ON p.pid=a.id
)
SELECT distinct * FROM menus order by id", mids);

            var list = DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<MenuInfo>>().QueryBySql(sql, null);
            return UserObject.MenuList = list as List<MenuInfo>;
        }

        /// <summary>
        /// 加载所有部门
        /// </summary>
        private List<DeptInfo> GetDept()
        {
            var list = DapperExt.IocContainer.Instance.Resolve<DapperExt.IRespositoryBase<List<DeptInfo>>>().Get(null);
            UserObject.DeptInfo = list.Find(o => o.Id == UserInfo.GroupID);
            return list;
        }

        /// <summary>
        /// 加载所有用户组
        /// </summary>
        private List<GroupInfo> GetUserGroup()
        {
            var list = DapperExt.IocContainer.Instance.Resolve<DapperExt.IRespositoryBase<List<GroupInfo>>>().Get(null);
            UserObject.GroupInfo = list.Find(o => o.Id == UserInfo.GroupID);
            return list;
        }

        /// <summary>
        /// 所有菜单及操作
        /// </summary>
        /// <returns></returns>
        private List<MenuOperation> GetMenuOperation()
        {
            var m = DapperExt.IocContainer.Instance.Resolve<Framework.DapperExt.IRespositoryBase<MenuOperation>>().GetList(null, null);
            UserObject.MenuOperationList = m as List<MenuOperation>;
            return UserObject.MenuOperationList;
        }

        #endregion

        /// <summary>
        /// 事件返回值
        /// </summary>
        /// <param name="Cursor"></param>
        /// <param name="Messages"></param>
        protected void LoginMeaus(int Cursor, string Messages)
        {
            LoginEventHandler?.Invoke(Cursor, Messages);
            Application.DoEvents();
        }

    }
}
