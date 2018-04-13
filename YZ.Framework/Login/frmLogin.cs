using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YZ.Framework
{
    public partial class frmLogin : Form
    {
        LoginUtil loginUtil = new LoginUtil();
        frmProgress frmProg = null;

        /// <summary>
        /// 构造
        /// </summary>
        public frmLogin()
        {
            InitializeComponent();
        }

        #region 初始化窗体

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                loginUtil.LoginEventHandler += new LoginUtil.LoginEventArgs(Login_LoginEventHandler);
            }
            catch
            { }
        }


        delegate void InvokeTextBox(int Cursor, string Messages);
        void Login_LoginEventHandler(int Cursor, string Messages)
        {
            Invoke(new InvokeTextBox(LoginEvnt), Cursor, Messages);
        }

        private void LoginEvnt(int Cursor, string Messages)
        {
            frmProg.LoginEventArgs(Cursor, Messages);
            switch (Cursor)
            {
                case 0:
                    MessageBox.Show(Messages, "系统提示");
                    frmProg.Close();
                    this.Show();
                    break;
                case 1:
                    MessageBox.Show(Messages, "系统提示");
                    //todo:
                    Process.Start(Application.StartupPath + "\\NetConfig.exe");
                    frmProg.Close();
                    frmProg = null;
                    this.Show();
                    break;
                case 2:
                    MessageBox.Show(Messages, "系统提示");
                    //todo:
                    Process.Start(Application.StartupPath + "\\AutoUpgrader.exe");
                    frmProg.Close();
                    this.Close();
                    break;
                case 3:
                    DialogResult result = MessageBox.Show(Messages, "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                    {
                        loginUtil.LoginEventHandler -= new LoginUtil.LoginEventArgs(Login_LoginEventHandler);

                        frmProg.LoginEventArgs(8, "系统正在关闭");
                        Application.Exit();
                        Process currentProcess = Process.GetCurrentProcess();
                        currentProcess.Kill();
                    }
                    break;
                case 4:
                    MessageBox.Show(Messages, "系统提示");
                    frmProg.LoginEventArgs(8, "系统正在关闭");
                    Application.Exit();
                    break;
            }
        }
        #endregion

        #region 登录系统
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        private bool Login()
        {
            try
            {
                loginUtil.UserName = this.txtUserName.Text.Trim();
                loginUtil.UserPass = this.txtPwd.Text.Trim();
                frmProg = new frmProgress();
                frmProg.Show();
                this.Hide();
                bool flag = loginUtil.Login();
                if (!flag)
                    return false;

                frmProg.Hide();
                Program.MainApp.Show();
                //默认加载首页
                //Program.MainApp.MenuInit("首页", "", "");
                return true;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
        }
        #endregion

        #region 退出系统
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region 敲入回车或TAB键时
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPwd.Focus();
                this.txtPwd.SelectAll();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.txtPwd.Focus();
                this.txtPwd.SelectAll();
            }
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(btnLogin, e);
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.btnLogin.Focus();
                this.btnLogin.Select();
            }
        }

        private void btnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Login();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.btnExit.Focus();
                this.btnExit.Select();
            }
        }

        private void btnExit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.Tab)
            {
                this.txtUserName.Focus();
                this.txtUserName.SelectAll();
            }
        }
        #endregion

        #region 整个页面可以拖动
        /// <summary>
        /// 整个页面可以拖动
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wparam, int lparam);
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)//按下的是鼠标左键            
            {
                Capture = false;//释放鼠标，使能够手动操作                
                SendMessage(Handle, 0x00A1, 2, 0);//拖动窗体            
            }
        }
        #endregion
    }
}