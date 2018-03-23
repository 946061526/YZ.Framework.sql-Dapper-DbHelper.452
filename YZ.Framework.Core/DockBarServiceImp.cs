using DevExpress.XtraBars.Docking;
using System.Windows.Forms;

namespace YZ.Framework.Core
{
    public class DockBarServiceImp : IDockPanel, IDockBarService
    {
        private ISystemApplication _SystemApplication = null;

        public DockBarServiceImp(ISystemApplication pSystemApplication) : base(pSystemApplication)
        {
            this._SystemApplication = pSystemApplication;
        }

        public void AddDockBar(DockBar dockBar)
        {
            BarFactroy barFactroy = new BarFactroy();
            barFactroy.SystemApplicaiton = this._SystemApplication;
            dockBar.SystemApplication = this._SystemApplication;
            dockBar.MdiParent = this._SystemApplication.MainFrom;
            dockBar.Show();
            //barFactroy.InitControl();
        }

        public void AddDocument(string dockBarName, NewForm NewdockBar)
        {
            base.AddDocuments(dockBarName, NewdockBar);
        }

        public void AddDockBar(string dockBarName, NewForm dockBar)
        {
            this.AddDocument(dockBarName, dockBar);
        }

        public void AddDockBar(string dockBarName, NewForm NewdockBar, DockState dockState)
        {
            switch (dockState)
            {
                case DockState.Float:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar);
                        break;
                    }
                case DockState.DockTopAutoHide:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Top);
                        dockPanel.Visibility = DockVisibility.AutoHide;
                        break;
                    }
                case DockState.DockLeftAutoHide:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Left);
                        dockPanel.Visibility = DockVisibility.AutoHide;
                        break;
                    }
                case DockState.DockBottomAutoHide:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Bottom);
                        dockPanel.Visibility = DockVisibility.AutoHide;
                        break;
                    }
                case DockState.DockRightAutoHide:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Right);
                        dockPanel.Visibility = DockVisibility.AutoHide;
                        break;
                    }
                case DockState.Document:
                    this.AddDocument(dockBarName, NewdockBar);
                    break;
                case DockState.DockTop:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Top);
                        break;
                    }
                case DockState.DockLeft:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Left);
                        break;
                    }
                case DockState.DockBottom:
                    base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Bottom);
                    break;
                case DockState.DockRight:
                    {
                        DockPanel dockPanel = base.AddDockPanel(dockBarName, NewdockBar, DockingStyle.Right);
                        break;
                    }
            }
        }

        public bool ActiveBar(NewForm dockBar)
        {
            bool flag = false;
            for (int i = 0; i < this._SystemApplication.MainDockManager.Panels.Count; i++)
            {
                if (this._SystemApplication.MainDockManager.Panels[i].ControlContainer != null)
                {
                    foreach (Control control in this._SystemApplication.MainDockManager.Panels[i].ControlContainer.Controls)
                    {
                        if (control is NewForm)
                        {
                            if (((NewForm)control).GetType().FullName.ToString() == dockBar.GetType().FullName.ToString())
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!flag)
            {
                dockBar.Activate();
            }
            return flag;
        }

        public DockBar IsNowActive()
        {
            return (DockBar)this._SystemApplication.ActiveForm;
        }

        public void IsActive(DockBar dockBar)
        {
            dockBar.Activate();
        }

        public int Count(DockBar DockBar)
        {
            int num = 0;
            for (int i = 0; i < this._SystemApplication.MainFrom.MdiChildren.Length; i++)
            {
                if (this._SystemApplication.MainFrom.MdiChildren[i] is DockBar && ((DockBar)this._SystemApplication.MainFrom.MdiChildren[i]).GetType().FullName.ToString() == DockBar.GetType().FullName.ToString())
                {
                    num++;
                }
            }
            for (int i = 0; i < this._SystemApplication.MainFrom.OwnedForms.Length; i++)
            {
                if (this._SystemApplication.MainFrom.OwnedForms[i] is DockBar && ((DockBar)this._SystemApplication.MainFrom.OwnedForms[i]).GetType().FullName.ToString() == DockBar.GetType().FullName.ToString())
                {
                    num++;
                }
            }
            return num;
        }
    }
}
