using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FancyCandles;
using IO.Swagger.Api;
using IO.Swagger.Model;
using LuckyCurrencyTest.Models;
using Newtonsoft.Json.Linq;
using Websocket.Client;

namespace LuckyCurrencyTest.Services
{
    static class Bybit
    {
        public static event Action<string> newMessage;
        private static Uri uri = new Uri("wss://stream.bybit.com/realtime");


        public static void RunBybitAsync()
        {
            //var exitEvent = new ManualResetEvent(false);

            WebsocketClient ws = new WebsocketClient(uri);
            ws.MessageReceived.Subscribe(message =>
            {
                newMessage(message.Text);
            });
            ws.Start();

            ws.Send("{\"op\":\"subscribe\",\"args\":[\"klineV2.1.BTCUSD\"]}");
            //exitEvent.WaitOne();
        }
        public static KlineBase GetKlineBase(string symbol, string interval)
        {
            var apiInstance = new KlineApi();
            long from = GetTimeServerSeconds() - 200 * 60;
            JObject result = (JObject) apiInstance.KlineGet(symbol, interval, from);

            return result.ToObject<KlineBase>();
        }

        public static long GetTimeServerSeconds()
        {
            var apiInstance = new CommonApi();
            JObject result = (JObject)apiInstance.CommonGetTime();
            double TimeServerSeconds = double.Parse(result.ToObject<ServerTime>().TimeNow);

            return (long)TimeServerSeconds;
        }

        public static long GetTimeDuration()
        {
            var timeServerTick = GetTimeServerSeconds() * 10000000;
            var timeNow = DateTime.Now.Ticks;

            return timeNow - timeServerTick;
        }

        public static ICandle GetCandleFromKlineRes(KlineRes kline)
        {
            DateTime openTime = new DateTime((long)kline.OpenTime * 10000000L + 621356075467488324L);

            return new Candle(openTime,
                double.Parse(kline.Open), double.Parse(kline.High), double.Parse(kline.Low),
                double.Parse(kline.Close), long.Parse(kline.Volume));
        }

        public static ICandle GetCandleFromKlineV2Res(KlineV2Res kline)
        {
            DateTime openTime = new DateTime(kline.start * 10000000L + 621356075467488324L);

            return new Candle(openTime, kline.open, kline.high, kline.low, kline.close, kline.volume);
        }

        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
    }
}
