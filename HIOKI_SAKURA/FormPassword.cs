using System;
using System.Drawing;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    public partial class FormPassword : Form
    {
        public string Password { get; private set; }
        private TextBox tbPassword;
        private Button btnOK;
        private Button btnCancel;

        public FormPassword()
        {
            this.Text = "工程模式密碼";
            this.Size = new Size(300, 130);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(10, 24, 48);
            this.Icon = Properties.Resources.SakuraICO;

            tbPassword = new TextBox { PasswordChar = '*', Location = new Point(20, 20), Width = 240 };
            btnOK = new Button
            {
                Text = "確定",
                Location = new Point(50, 60),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOK.FlatAppearance.BorderSize = 0;

            btnCancel = new Button
            {
                Text = "取消",
                Location = new Point(150, 60),
                DialogResult = DialogResult.Cancel,
                BackColor = Color.FromArgb(200, 50, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.FlatAppearance.BorderSize = 0;

            btnOK.FlatAppearance.MouseOverBackColor = Color.FromArgb(0, 150, 255);
            btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 70, 70);

            this.Controls.Add(tbPassword);
            this.Controls.Add(btnOK);
            this.Controls.Add(btnCancel);

            btnOK.Click += (s, e) => { Password = tbPassword.Text; };

            // =============================
            // 按 Enter 等同按確定
            // =============================
            tbPassword.KeyDown += TbPassword_KeyDown;
        }

        private void TbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // 阻止 beep
                btnOK.PerformClick();      // 執行確定按鈕
            }
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
