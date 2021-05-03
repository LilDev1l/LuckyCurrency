using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Websocket.Client;

namespace LuckyCurrencyTestConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);
            var url = new Uri("wss://stream.bytick.com/realtime_public");

            using (var client = new WebsocketClient(url))
            {

                /*                client.ReconnectTimeout = TimeSpan.FromSeconds(50);
                                client.ReconnectionHappened.Subscribe(info =>
                                    Console.WriteLine($"Reconnection happened, type: {info.Type}"));
                */

                client.MessageReceived.Subscribe(msg =>
                {
                    Console.WriteLine($"Message received: {msg}");
                });
                client.Start();

                client.Send("{\"op\":\"subscribe\",\"args\":[\"trade.BTCUSDT\"]}");
                //exitEvent.WaitOne();
                Thread.Sleep(10000);
                client.Stop(System.Net.WebSockets.WebSocketCloseStatus.Empty, "stop");
            }
        }
    }
}
