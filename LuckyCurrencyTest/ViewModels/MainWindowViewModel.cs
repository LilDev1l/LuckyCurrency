using LuckyCurrencyTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Threading;
using FancyCandles;
using IO.Swagger.Model;
using LuckyCurrencyTest;
using LuckyCurrencyTest.Models;
using LuckyCurrencyTest.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LuckyCurrencyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Торговая пара
        private string _selectedTab;
        public string SelectedTab
        {
            get => _selectedTab;
            set => Set(ref _selectedTab, value);
        }
        #endregion

        #region Свечки
        private ObservableCollection<ICandle> candles;
        public ObservableCollection<ICandle> Candles
        {
            get => candles;
            set => Set(ref candles, value);
        }

        public static long timestampOpen;
        //public object CandlesLock { get; } = new object();
        #endregion

        public MainWindowViewModel()
        {

            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            Bybit.SetCultureUS();

            LinearKlineRespBase klineBase = Bybit.GetLinearKlineBase("BTCUSDT", "1");
            foreach (var kline in ((JArray)klineBase.Result).ToObject<List<LinearKlineResp>>())
            {
                candles.Add(Bybit.GetCandleFromLinearKlineResp(kline));
            }

            Candles = candles;
            //BindingOperations.EnableCollectionSynchronization(Candles, CandlesLock);

            Bybit.newMessage += OnGetNewMessage;
            Bybit.RunBybitAsync();
        }

        public void OnGetNewMessage(string message)
        {
            if (message.Contains("\"topic\":\"candle.1.BTCUSDT\""))
            {
                Console.WriteLine("New Message: " + message);

                KlineV2Base klineV2Base = JsonConvert.DeserializeObject<KlineV2Base>(message);

                if (klineV2Base.data[0].confirm)
                {
                    if (timestampOpen == klineV2Base.data[1].start)
                        return;
                    else
                    { 
                        timestampOpen = klineV2Base.data[1].start;
                        Console.WriteLine($"timestampOpen изменен на ({timestampOpen})");
                        OnChangeLastCandle(klineV2Base.data[0]);
                        OnAddNewCandle(klineV2Base.data[1]);
                    }
                }
                else
                {
                    OnChangeLastCandle(klineV2Base.data[0]);
                }
            }
        }

        public void OnChangeLastCandle(KlineV2Res klineV2)
        {
            ICandle candle = Bybit.GetCandleFromKlineV2Res(klineV2);
            int lastCandleCount = Candles.Count - 1;

            App.Current.Dispatcher.InvokeAsync(() =>
            {
/*                lock (CandlesLock)
                {*/
                Candles[lastCandleCount] = candle;
/*                }*/
                Console.WriteLine("Изменено: " + candle);
            });
        }

        public void OnAddNewCandle(KlineV2Res klineV2)
        {
            ICandle candle = Bybit.GetCandleFromKlineV2Res(klineV2);

            App.Current.Dispatcher.InvokeAsync(() =>
            {
/*                lock (CandlesLock)
                {*/
                Candles.Add(candle);
/*                }*/
                Console.WriteLine("Добавлено: " + candle);
            });
        }
    }
}
