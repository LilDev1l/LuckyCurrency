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

            var apiInstanceTime = new CommonApi();
            var apiInstance = new KlineApi();
            var timeServerSeconds = (long)((JObject) apiInstanceTime.CommonGetTime())["time_now"].ToObject<double>();
            var timeServerTick = timeServerSeconds * 10000000;
            var timeNow = DateTime.Now.Ticks;
            var timeDuration = timeNow - timeServerTick;

            var symbol = "BTCUSD";  // string | Contract type.
            var interval = "1";  // string | Kline interval.
            var from = timeServerSeconds - 200 * 60;  // decimal? | from timestamp.
            var limit = 200;  // decimal? | Contract type. (optional) 

            try
            {
                // Get kline
                Object result = apiInstance.KlineGet(symbol, interval, (int)from, limit);
                if (result is JObject jo)
                {
                    //Console.WriteLine(jo);
                    List<KlineRes> klines = jo.ToObject<KlineBase>().Result;
                    int i = 0;
                    foreach (var kline in klines)
                    {
                        Console.WriteLine(kline);
                        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                        ; 
                        
                        candles.Add(new Candle(new DateTime((long)kline.OpenTime * 10000000 + timeDuration), double.Parse(kline.Open), double.Parse(kline.High), double.Parse(kline.Low), double.Parse(kline.Close), long.Parse(kline.Volume)));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling KlineApi.KlineGet: " + e.Message);
            }

            DataContext = candles;
        }
    }
}

