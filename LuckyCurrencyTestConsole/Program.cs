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
            var url = new Uri("wss://stream-testnet.bybit.com/realtime");

            using (var client = new WebsocketClient(url))
            {
                client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
                client.Start();

                client.Send("{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.BTCUSD\"]}");

                Thread.Sleep(2000);

                Thread.Sleep(2000);
                Console.WriteLine($"ОТКЛЮЧЕНИЕ###################################################################");
                client.Reconnect();
                Console.WriteLine($"ОТКЛЮЧЕНИЕ###################################################################");

                Console.Read();
            }
        }
    }
}
