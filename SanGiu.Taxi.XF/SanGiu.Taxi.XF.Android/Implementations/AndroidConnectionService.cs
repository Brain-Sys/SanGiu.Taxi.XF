using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Plugin.LocalNotifications;
using SanGiu.Taxi.Interfaces;
using Sockets.Plugin;
using Xamarin.Forms;

[assembly: Dependency(typeof(SanGiu.Taxi.XF.Droid.Implementations.AndroidConnectionService))]
namespace SanGiu.Taxi.XF.Droid.Implementations
{
    public class AndroidConnectionService : Service, IConnectionService
    {
        string address = "192.168.15.109";
        int port = 11000;
        Random r = new Random();
        TcpSocketClient client;

        public int BytesRead { get; set; }

        public event EventHandler<object> SomethingHappened;

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return base.OnStartCommand(intent, flags, startId);
        }

        public async Task StartAsync()
        {
            client = new TcpSocketClient();
            await client.ConnectAsync(address, port);
            backgroundTask();
            this.SomethingHappened?.Invoke(this, "Connection successful...");
        }

        public async Task StopAsync()
        {
            await client.DisconnectAsync();
            client.Dispose();
        }

        public async Task WriteAsync(byte value)
        {
            if (client != null) return;

            client.WriteStream.WriteByte(value);
            await client.WriteStream.FlushAsync();
        }

        private async void backgroundTask()
        {
            if (client == null) return;

            await Task.Run(async () =>
            {
                while (true)
                {
                    byte[] buffer = new byte[1];
                    await client.ReadStream.ReadAsync(buffer, 0, 1);
                    this.BytesRead = buffer.Length;

                    CrossLocalNotifications.Current.Show("title", "body", 101);

                    this.SomethingHappened?.Invoke(this, buffer);
                }
            });
        }
    }
}