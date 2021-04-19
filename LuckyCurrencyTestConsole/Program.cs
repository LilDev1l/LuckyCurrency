using System;
using System.Threading;
using System.Threading.Tasks;

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
                client.ReconnectTimeout = TimeSpan.FromSeconds(30);
                client.ReconnectionHappened.Subscribe(info =>
                    Console.WriteLine($"Reconnection happened, type: {info.Type}"));

                client.MessageReceived.Subscribe(msg => Console.WriteLine($"Message received: {msg}"));
                client.Start();

                Task.Run(() => client.Send("{\"op\": \"subscribe\", \"args\": [\"orderBookL2_25.BTCUSD\"]}"));

                exitEvent.WaitOne();
                Console.Read();
            }
        }
    }
}
