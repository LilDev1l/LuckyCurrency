using System.Windows;
using System.Collections.ObjectModel;
using FancyCandles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Threading;
using Newtonsoft.Json.Linq;
using IO.Swagger.Model;
using IO.Swagger.Api;

namespace LuckyCurrencyTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            SetCultureUS();

            KlineBase klineBase = GetKlineBase("XRPUSD", "1");
            foreach (var kline in klineBase.Result)
            {
                candles.Add(GetCandleFromKlineRes(kline));
            }

            DataContext = candles;
        }

        public static KlineBase GetKlineBase(string symbol, string interval)
        {
            var apiInstance = new KlineApi();
            long from = GetTimeServerSeconds() - 200 * 60;
            JObject result = (JObject)apiInstance.KlineGet(symbol, interval, from);

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

        public static Candle GetCandleFromKlineRes(KlineRes kline)
        {
            DateTime openTime = new DateTime((long)kline.OpenTime * 10000000L + GetTimeDuration());

            return new Candle(openTime,
                double.Parse(kline.Open), double.Parse(kline.High), double.Parse(kline.Low),
                double.Parse(kline.Close), long.Parse(kline.Volume));
        }

        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
    }
}

