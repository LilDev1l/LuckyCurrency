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

namespace LuckyCurrencyTestConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*HttpClient client = new HttpClient();

            WebRequest request = WebRequest.Create("https://api.bybit.com/v2/public/kline/list?symbol=BTCUSD&interval=1&limit=1&from=1581231260");
            WebResponse response = request.GetResponseAsync().Result;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Console.WriteLine(reader.ReadToEnd());
                }
            }
            response.Close();*/
            var apiInstance = new KlineApi();
            var symbol = "BTCUSD";  // string | Contract type.
            var interval = "1";  // string | Kline interval.
            var from = 1581231260;  // decimal? | from timestamp.
            var limit = 10;  // decimal? | Contract type. (optional) 

            try
            {
                // Get kline
                Object result = apiInstance.KlineGet(symbol, interval, from, limit);
                Console.WriteLine(result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception when calling KlineApi.KlineGet: " + e.Message);
            }


            Console.Read();
        }

        public static async void GetCandlesAsync()
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("https://api.bybit.com/v2/public/kline/list?symbol=BTCUSD&interval=1&limit=2&from=1581231260");

            Console.WriteLine(response);

            Object obj = JsonConvert.DeserializeObject<Object>(response);
        }
    }
}
