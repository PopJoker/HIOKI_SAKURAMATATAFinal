using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    public partial class FormMqttSetting : Form
    {
        public string Broker { get; private set; }
        public int Port { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        public FormMqttSetting(string broker, int port, string username, string password)
        {
            InitializeComponent();

            tbBroker.Text = broker;
            numPort.Value = port;
            tbUsername.Text = username;
            tbPassword.Text = password;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Broker = tbBroker.Text.Trim();
            Port = (int)numPort.Value;
            Username = tbUsername.Text.Trim();
            Password = tbPassword.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string testBroker = tbBroker.Text.Trim();
            int testPort = (int)numPort.Value;
            string testUser = tbUsername.Text.Trim();
            string testPass = tbPassword.Text;

            try
            {
                // 使用 M2Mqtt 套件範例
                var client = new uPLibrary.Networking.M2Mqtt.MqttClient(testBroker, testPort, false, null, null, uPLibrary.Networking.M2Mqtt.MqttSslProtocols.None);

                if (!string.IsNullOrEmpty(testUser))
                    client.Connect(Guid.NewGuid().ToString(), testUser, testPass);
                else
                    client.Connect(Guid.NewGuid().ToString());

                if (client.IsConnected)
                {
                    MessageBox.Show("連線成功！", "測試結果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    client.Disconnect();
                }
                else
                {
                    MessageBox.Show("連線失敗！", "測試結果", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"連線失敗：{ex.Message}", "測試結果", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
