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
using LuckyCurrencyTest.Services.Models;
using Newtonsoft.Json.Linq;
using Websocket.Client;

namespace LuckyCurrencyTest.Services
{
    static class Bybit
    {
        #region LinearKline
        public static ObservableCollection<ICandle> GetCandles(string symbol, string interval)
        {
            SetCultureUS();
            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            LinearKlineBase klineBase = GetLinearKlineBase(symbol, interval);
            foreach(var kline in klineBase.Result)
            {
                candles.Add(GetCandleFromLinearKline(kline));
            }

            return candles;
        }
        public static long GetIntervalSeconds(string interval)
        {
            switch (interval)
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
        public static LinearKlineBase GetLinearKlineBase(string symbol, string interval)
        {
            var apiInstance = new LinearKlineApi();
            long from = GetTimeServerSeconds() - 200 * GetIntervalSeconds(interval);
            JObject result = (JObject)apiInstance.LinearKlineGet(symbol, interval, from);

            return result.ToObject<LinearKlineBase>();
        }
        public static ICandle GetCandleFromLinearKline(LinearKline kline)
        {
            DateTime openTime = new DateTime((long)kline.OpenTime * 10000000L + 621356075467488324L);

            return new Candle(openTime, kline.Open.Value, kline.High.Value, kline.Low.Value, kline.Close.Value, (long)kline.Volume.Value);
        }
        #endregion
        #region Server
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
        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        #endregion
    }
}
