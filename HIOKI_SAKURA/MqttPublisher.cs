using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIOKI_SAKURA
{
    public class MqttPublisher
    {
        private IMqttClient client;
        private IMqttClientOptions options;
        private bool connected = false;
        public bool IsConnected => connected;

        public MqttPublisher(string broker, int port, string username = null, string password = null)
        {
            var factory = new MqttFactory();
            client = factory.CreateMqttClient();

            var builder = new MqttClientOptionsBuilder()
                .WithTcpServer(broker, port)
                .WithClientId($"HIOKI_{Guid.NewGuid()}")
                .WithCleanSession();

            if (!string.IsNullOrEmpty(username))
                builder.WithCredentials(username, password);

            options = builder.Build();

            client.UseConnectedHandler(e => connected = true);
            client.UseDisconnectedHandler(e => connected = false);
        }

        public async Task ConnectAsync()
        {
            try
            {
                await client.ConnectAsync(options, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MQTT 連線失敗: {ex.Message}");
            }
        }

        public async Task PublishAsync(string topic, object payload)
        {
            if (!connected)
            {
                NotifyIcon notifyIcon = new NotifyIcon
                {
                    Visible = true,
                    Icon = Properties.Resources.SakuraICO, // 或自訂 icon
                    BalloonTipTitle = "MQTT 提示",
                    BalloonTipText = "MQTT 尚未連線，資料暫時未發佈。"
                };
                notifyIcon.ShowBalloonTip(3000);
                return;
            }

            var message = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
                .WithPayload(JsonSerializer.Serialize(payload))
                .WithExactlyOnceQoS()
                .WithRetainFlag(false)
                .Build();

            try
            {
                await client.PublishAsync(message, CancellationToken.None);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"MQTT 發佈失敗: {ex.Message}", "MQTT 錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public async Task DisconnectAsync()
        {
            if (connected)
                await client.DisconnectAsync();
        }
    }
}
