using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YZ.Framework
{
    public partial class frmProgress : Form
    {
        private Label lblMsg = null;

        public frmProgress()
        {
            InitializeComponent();
            this.progressBarControl1.Properties.Minimum = 0;
            this.progressBarControl1.Position = 0;
        }

        public void LoginEventArgs(int Flag, string Message)
        {
            this.Cursor = Cursors.WaitCursor;
            if (Flag != 9)
                this.progressBarControl1.PerformStep();
            lblMsg.Text = Message.Trim();
            Application.DoEvents();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            lblMsg = new Label();
            this.lblMsg.AutoSize = true;
            this.lblMsg.BackColor = System.Drawing.Color.Transparent;
            this.lblMsg.ForeColor = System.Drawing.Color.Red;
            this.lblMsg.Location = new System.Drawing.Point(12, 125);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(41, 12);
            this.lblMsg.TabIndex = 18;
            this.lblMsg.Text = "label1";
            this.pictureEdit1.Controls.Add(lblMsg);
        }
    }
}

