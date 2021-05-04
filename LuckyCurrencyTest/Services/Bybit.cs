using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using LuckyCurrencyTest.Services.Models.LinearKline;
using LuckyCurrencyTest.Services.Models.LinearKlineWebSocket;
using LuckyCurrencyTest.Services.Authentication;
using Newtonsoft.Json.Linq;
using Websocket.Client;

namespace LuckyCurrencyTest.Services
{
    static class Bybit
    {
        private static WebsocketClient _wsPublic;
        private static WebsocketClient _wsPrivate;
        private static long _duration; 
        public static event Action<string> NewMessage;

        #region Статический конструктор
        static Bybit()
        {
            SetCultureUS();
            _duration = GetTimeDuration();
        }
        #endregion

        #region WebSocket
        public static void RunBybitWebSocket()
        {
            string api_key = "QIqhha0rxn2MsE9RVy";
            string secret = "DdG6XxKhIbchVRvEFmFOyazlyRCnqESGA1Pa";
            long expires = DateTime.Now.Ticks + 5000;
            string signature = Auth.CreateSignature(secret, "GET/realtime" + expires);
            _wsPublic = new WebsocketClient(new Uri("wss://stream.bytick.com/realtime_public"));
            _wsPrivate = new WebsocketClient(new Uri($"wss://stream.bytick.com/realtime_private?api_key={api_key}&expires={expires}&signature={signature}"));
            _wsPublic.MessageReceived.Subscribe(message =>
            {
                NewMessage(message.Text);
            });
            _wsPrivate.MessageReceived.Subscribe(message =>
            {
                NewMessage(message.Text);
            });
            _wsPublic.Start();
            _wsPrivate.Start();
            //_wsPrivate.Send($"{{\"op\":\"auth\",\"args\":[\"{api_key}\",\"{expires}\",\"{signature}\"]}}");
            _wsPrivate.Send("{\"op\":\"subscribe\",\"args\":[\"wallet\"]}");
/*            SendMessage("{\"op\":\"subscribe\",\"args\":[\"candle.1.BTCUSDT\"]}");
            SendMessage("{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.BTCUSDT\"]}");
            SendMessage("{\"op\":\"subscribe\",\"args\":[\"trade.BTCUSDT\"]}");*/
        }

        public static void SendMessage(string message)
        {
            _wsPublic.Send(message);
        }
        public static void ReconnectWebSocket()
        {
            _wsPublic.Reconnect();
        }

        public static ICandle GetCandleFromLinearKlineWebSocket(LinearKlineWebSocketData klineWebSocket)
        {
            DateTime openTime = new DateTime(klineWebSocket.Start * 10000000L + _duration);

            return new Candle(openTime, klineWebSocket.Open, klineWebSocket.High, klineWebSocket.Low, klineWebSocket.Close, (int)double.Parse(klineWebSocket.Volume));
        }
        #endregion
        
        #region LinearKline
        public static ObservableCollection<ICandle> GetCandles(string pair, string timeframe)
        {
            SetCultureUS();
            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            
            LinearKlineBase klineBase = GetLinearKlineBase(pair, timeframe);
            foreach(var kline in klineBase.Result)
            {
                candles.Add(GetCandleFromLinearKline(kline));
            }

            return candles;
        }
        public static long GetIntervalSeconds(string timeframe)
        {
            switch (timeframe)
            {
                case "1":
                    return 1 * 60;
                case "3":
                    return 3 * 60;
                case "5":
                    return 5 * 60;
                case "15":
                    return 15 * 60;
                case "30":
                    return 30 * 60;
                case "60":
                    return 60 * 60;
                case "120":
                    return 120 * 60;
                case "240":
                    return 240 * 60;
                case "360":
                    return 360 * 60;
                case "D":
                    return 24 * 60 * 60;
                case "W":
                    return 7 * 24 * 60 * 60;
                case "M":
                    return 30 * 24 * 60 * 60;
                default:
                    throw new Exception("Неверный формат интервала");
            }

        }
        public static LinearKlineBase GetLinearKlineBase(string pair, string timeframe)
        {
            var apiInstance = new LinearKlineApi();
            long from = GetTimeServerSeconds() - 200 * GetIntervalSeconds(timeframe);
            JObject result = (JObject)apiInstance.LinearKlineGet(pair, timeframe, from);

            return result.ToObject<LinearKlineBase>();
        }
        public static ICandle GetCandleFromLinearKline(LinearKlineData kline)
        {
            DateTime openTime = new DateTime(kline.OpenTime.Value * 10000000L + _duration);

            return new Candle(openTime, kline.Open.Value, kline.High.Value, kline.Low.Value, kline.Close.Value, (long)kline.Volume.Value);
        }
        #endregion
        
        #region Server
        public static long GetTimeServerSeconds()
        {
            var apiInstance = new CommonApi();
            JObject result = (JObject)apiInstance.CommonGetTime();
            double timeServerSeconds = double.Parse(result.ToObject<ServerTime>().TimeNow);

            return (long)timeServerSeconds;
        }

        public static long GetTimeDuration()
        {
            var timeServerTick = GetTimeServerSeconds() * 10000000;
            var timeNow = DateTime.Now.Ticks;

            return timeNow - timeServerTick;
        }
        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        #endregion
    }
}
