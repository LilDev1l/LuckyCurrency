using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;
using Newtonsoft.Json.Linq;

namespace LuckyCurrencyTestConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            var apiInstance = new KlineApi();
            var symbol = "BTCUSD";  // string | Contract type.
            var interval = "1";  // string | Kline interval.
            var from = 1581231260;  // decimal? | from timestamp.
            var limit = 10;  // decimal? | Contract type. (optional) 

            try
            {
                // Query mark price kline.
                Object result = apiInstance.KlineMarkPrice(symbol, interval, from, limit);
                if (result is JObject jo)
                {
                    Console.WriteLine(jo);
                    List<MarkPriceKlineInfo> klines = jo["result"].ToObject<List<MarkPriceKlineInfo>>();
                    foreach (var kline in klines)
                    {
                        Console.WriteLine(kline);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling KlineApi.KlineGet: " + e.Message);
            }

            Console.Read();
        }
    }
}
