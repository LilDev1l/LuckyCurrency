using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IO.Swagger.Api;
using LuckyCurrency.Services.Models.LinearKline;
using LuckyCurrency.Services.Models.LinearKlineWebSocket;
using LuckyCurrency.Services.Auth;
using Newtonsoft.Json.Linq;
using Websocket.Client;
using IO.Swagger.Client;
using LuckyCurrency.Services.Models.Balance;
using LuckyCurrency.Services.Models.ServerTime;

namespace LuckyCurrency.Services
{
    static class Bybit
    {
        private static WebsocketClient _wsPublic;
        private static WebsocketClient _wsPrivate;
        public static long Duration { get; set; } 
        public static event Action<string> NewMessage;

        private static string api_key = "QIqhha0rxn2MsE9RVy";
        private static string secret = "DdG6XxKhIbchVRvEFmFOyazlyRCnqESGA1Pa";

        #region Статический конструктор
        static Bybit()
        {
            SetCultureUS();
            Duration = GetTimeDuration();
        }
        #endregion

        #region WebSockets
        public static void RunBybitWS()
        {
            long expires = DateTime.Now.Ticks + 1000;
            string signature = Authentication.CreateSignature(secret, "GET/realtime" + expires);
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

            SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"wallet\"]}");
            SendPublicWS("{\"op\":\"subscribe\",\"args\":[\"candle.1.BTCUSDT\"]}");
            SendPublicWS("{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.BTCUSDT\"]}");
            SendPublicWS("{\"op\":\"subscribe\",\"args\":[\"trade.BTCUSDT\"]}");
        }

        #region public
        public static void SendPublicWS(string message)
        {
            _wsPublic.Send(message);
        }
        public static void ReconnectPublicWS()
        {
            _wsPublic.Reconnect();
        }
        #endregion
        #region private
        public static void SendPrivateWS(string message)
        {
            _wsPrivate.Send(message);
        }
        public static void ReconnectPrivateWS()
        {
            _wsPrivate.Reconnect();
        }
        #endregion

        #endregion

        #region HTTP

        #region public

        #region LinearKline
        public static LinearKlineBase GetLinearKlineBase(string pair, string timeframe)
        {
            var apiInstance = new LinearKlineApi();
            long from = GetTimeServer(Time.Seconds) - 200 * GetIntervalSeconds(timeframe);
            JObject result = (JObject)apiInstance.LinearKlineGet(pair, timeframe, from);

            return result.ToObject<LinearKlineBase>();
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
        #endregion

        #endregion
        #region private

        #region Balance
        public static BalanceBase GetCurrentBalanceBase(string coin)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            Configuration.Default.AddApiKey("api_key", api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(secret, $"api_key={api_key}&coin={coin}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new WalletApi();
            JObject result = (JObject)apiInstance.WalletGetBalance(coin);

            return result.ToObject<BalanceBase>();
        }
        #endregion

        #endregion

        #region Server
        public enum Time
        {
            Seconds,
            MiliSeconds,
            Tick
        }
        public static long GetTimeServer(Time time)
        {
            var apiInstance = new CommonApi();
            JObject result = (JObject)apiInstance.CommonGetTime();
            long timeServerSeconds = (long)result.ToObject<ServerTimeData>().Time_now;

            switch (time)
            {
                case Time.Seconds:
                    return timeServerSeconds;
                case Time.MiliSeconds:
                    return timeServerSeconds * 1000;
                case Time.Tick:
                    return timeServerSeconds * 10000000;
                default:
                    throw new Exception("Неверный формат времени");
            }
        }

        public static long GetTimeDuration()
        {
            var timeServerTick = GetTimeServer(Time.Tick);
            var timeNow = DateTime.Now.Ticks;

            return timeNow - timeServerTick;
        }
        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        #endregion

        #endregion
    }
}
