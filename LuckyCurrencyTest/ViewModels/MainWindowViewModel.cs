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

        public static long timestamp;
        #endregion

        public MainWindowViewModel()
        {

            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            Bybit.SetCultureUS();

            KlineBase klineBase = Bybit.GetKlineBase("BTCUSD", "1");
            foreach (var kline in klineBase.Result)
            {
                candles.Add(Bybit.GetCandleFromKlineRes(kline));
            }

            Candles = candles;

            Bybit.newMessage += OnGetNewMessage;
            Bybit.RunBybitAsync();
        }

        public void OnGetNewMessage(string message)
        {
            if (message.Contains("\"topic\":\"klineV2.1.BTCUSD\""))
            {
                KlineV2Base klineV2Base = JsonConvert.DeserializeObject<KlineV2Base>(message);
                if(timestamp == klineV2Base.data[0].timestamp)
                    return;

                timestamp = klineV2Base.data[0].timestamp;
                if (klineV2Base.data[0].confirm)
                {
                    OnChangeLastCandle(klineV2Base.data[0]);
                    OnAddNewCandle(klineV2Base.data[1]);
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
            Console.WriteLine("Изменено: " + candle);

            int lastCandleCount = Candles.Count - 1;
            App.Current.Dispatcher.InvokeAsync(() => Candles[lastCandleCount] = candle);
        }

        public void OnAddNewCandle(KlineV2Res klineV2)
        {
            ICandle candle = Bybit.GetCandleFromKlineV2Res(klineV2);
            Console.WriteLine("Добавлено: " + candle);

            App.Current.Dispatcher.InvokeAsync(() => Candles.Add(candle));
        }
    }
}
