using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;
using FancyCandles;
using IO.Swagger.Api;
using IO.Swagger.Model;
using LuckyCurrencyTest.Services.Auth;
using Newtonsoft.Json.Linq;
using Websocket.Client;
using IO.Swagger.Client;

namespace LuckyCurrencyTest.Services
{
    static class Bybit
    {
        private static WebsocketClient _ws;
        private static long _duration; 
        public static event Action<string> NewMessage;

        private static string api_key = "QIqhha0rxn2MsE9RVy";
        private static string secret = "DdG6XxKhIbchVRvEFmFOyazlyRCnqESGA1Pa";

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
            
        }
        #endregion

        #region HTTP

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

        #endregion
    }
}
