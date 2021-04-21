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
            var url = new Uri("wss://stream.bybit.com/realtime");

            using (var client = new WebsocketClient(url))
            {

                /*                client.ReconnectTimeout = TimeSpan.FromSeconds(50);
                                client.ReconnectionHappened.Subscribe(info =>
                                    Console.WriteLine($"Reconnection happened, type: {info.Type}"));
                */

                List<Candle> candles = new List<Candle>();
                candles.Add(new Candle(DateTime.Now, 0, 0, 0, 0, 0));
                long timestamp = 0;
                client.MessageReceived.Subscribe(msg =>
                {
                    if (msg.Text.Contains("\"topic\":\"klineV2.1.BTCUSD\""))
                    {
                        KlineV2Base klineV2Base = JsonConvert.DeserializeObject<KlineV2Base>(msg.Text);

                        foreach (var klineV2 in klineV2Base.data)
                        {
                            Candle candle = candles[candles.Count - 1];
                            candle.C = klineV2.close;
                            candle.H = klineV2.high;
                            candle.O = klineV2.open;
                            candle.L = klineV2.low;
                            candle.V = klineV2.volume;
                            candle.t = new DateTime(klineV2.start);
                            Console.WriteLine("Изменено: " + candle);
                            if (klineV2.confirm)
                            {
                                candles.Add(new Candle(new DateTime(klineV2.start), klineV2.open, klineV2.high,
                                    klineV2.low, klineV2.close, klineV2.volume));
                                Console.WriteLine("Последняя информация о свечке получена:");
                                timestamp = klineV2.timestamp;
                            }
                        }
                    }

                    //Console.WriteLine($"Message received: {msg}");
                    });
                client.Start();

                Task.Run(() => client.Send("{\"op\":\"subscribe\",\"args\":[\"klineV2.1.BTCUSD\"]}"));
                Task.Run(() =>
                {
                    while (true)
                    {
                        Thread.Sleep(20000);
                        client.Send("{\"op\":\"ping\"}");
                    }
                });

                exitEvent.WaitOne();
                Console.Read();
            }
        }
    }
}
