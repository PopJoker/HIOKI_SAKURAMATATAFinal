using System.Drawing;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    partial class FormMqttSetting
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox grpMqtt;
        private Label lblBroker, lblPort, lblUsername, lblPassword;
        private TextBox tbBroker, tbUsername, tbPassword;
        private NumericUpDown numPort;
        private Button btnConnect, btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.grpMqtt = new System.Windows.Forms.GroupBox();
            this.lblBroker = new System.Windows.Forms.Label();
            this.tbBroker = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.grpMqtt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // grpMqtt
            // 
            this.grpMqtt.BackColor = System.Drawing.Color.White;
            this.grpMqtt.Controls.Add(this.btnTest);
            this.grpMqtt.Controls.Add(this.lblBroker);
            this.grpMqtt.Controls.Add(this.tbBroker);
            this.grpMqtt.Controls.Add(this.lblPort);
            this.grpMqtt.Controls.Add(this.numPort);
            this.grpMqtt.Controls.Add(this.lblUsername);
            this.grpMqtt.Controls.Add(this.tbUsername);
            this.grpMqtt.Controls.Add(this.lblPassword);
            this.grpMqtt.Controls.Add(this.tbPassword);
            this.grpMqtt.Controls.Add(this.btnConnect);
            this.grpMqtt.Controls.Add(this.btnCancel);
            this.grpMqtt.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold);
            this.grpMqtt.ForeColor = System.Drawing.Color.Navy;
            this.grpMqtt.Location = new System.Drawing.Point(20, 20);
            this.grpMqtt.Name = "grpMqtt";
            this.grpMqtt.Size = new System.Drawing.Size(400, 250);
            this.grpMqtt.TabIndex = 0;
            this.grpMqtt.TabStop = false;
            this.grpMqtt.Text = "MQTT 設定";
            // 
            // lblBroker
            // 
            this.lblBroker.Location = new System.Drawing.Point(20, 40);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(80, 25);
            this.lblBroker.TabIndex = 0;
            this.lblBroker.Text = "Broker:";
            // 
            // tbBroker
            // 
            this.tbBroker.Location = new System.Drawing.Point(120, 35);
            this.tbBroker.Name = "tbBroker";
            this.tbBroker.Size = new System.Drawing.Size(250, 34);
            this.tbBroker.TabIndex = 1;
            // 
            // lblPort
            // 
            this.lblPort.Location = new System.Drawing.Point(20, 80);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(80, 25);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "Port:";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(120, 75);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(120, 34);
            this.numPort.TabIndex = 3;
            this.numPort.Value = new decimal(new int[] {
            1883,
            0,
            0,
            0});
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(20, 120);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(90, 25);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username:";
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(120, 115);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(250, 34);
            this.tbUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(20, 160);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(90, 25);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password:";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(120, 155);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(250, 34);
            this.tbPassword.TabIndex = 7;
            this.tbPassword.UseSystemPasswordChar = true;
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.btnConnect.FlatAppearance.BorderSize = 0;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.ForeColor = System.Drawing.Color.White;
            this.btnConnect.Location = new System.Drawing.Point(120, 200);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 35);
            this.btnConnect.TabIndex = 8;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.LightGray;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.Black;
            this.btnCancel.Location = new System.Drawing.Point(250, 200);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.btnTest.FlatAppearance.BorderSize = 0;
            this.btnTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTest.ForeColor = System.Drawing.Color.White;
            this.btnTest.Location = new System.Drawing.Point(25, 200);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(85, 35);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // FormMqttSetting
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(24)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.grpMqtt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMqttSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MQTT 設定";
            this.grpMqtt.ResumeLayout(false);
            this.grpMqtt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Button btnTest;
    }
}
