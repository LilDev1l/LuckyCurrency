using LuckyCurrencyTest.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using FancyCandles;
using LuckyCurrencyTest.Services;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using LuckyCurrencyTest.Infrastructure.Commands;
using LuckyCurrencyTest.Services.Models.LinearKlineWebSocket;

namespace LuckyCurrencyTest.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Торговая пара
        private ComboBoxItem _selectedPair;
        public ComboBoxItem SelectedPair
        {
            get => _selectedPair;
            set => Set(ref _selectedPair, value);
        }
        #endregion

        #region Таймфрейм
        private ComboBoxItem _selectedTimeframe;
        public ComboBoxItem SelectedTimeframe
        {
            get => _selectedTimeframe;
            set => Set(ref _selectedTimeframe, value);
        }
        #endregion

        #region Свечи
        private ObservableCollection<ICandle> _candles;
        private static long _timestampOpen;
        public ObservableCollection<ICandle> Candles
        {
            get => _candles;
            set => Set(ref _candles, value);
        }
        #endregion

        #region Команды

        #region ChangePairOrTimeframeCommand
        public ICommand ChangePairOrTimeframeCommand { get; }
        private bool CanChangePairOrTimeframeCommandExecute(object p) => true;
        private void OnChangePairOrTimeframeCommandExecuted(object p)
        {
            Candles = Bybit.GetCandles(SelectedPair.Content.ToString(), SelectedTimeframe.Content.ToString());
        }
        #endregion

        #region RunWebSocketCommand
        public ICommand RunWebSocketCommand { get; }
        private bool CanRunWebSocketCommandExecute(object p) => true;
        private void OnRunWebSocketCommandExecuted(object p)
        {
            
            Bybit.NewMessage += OnGetNewMessage;
            Bybit.RunBybitWebSocket();
        }
        #endregion

        #endregion
        public MainWindowViewModel()
        {
            #region Команды
            ChangePairOrTimeframeCommand = new LambdaCommand(OnChangePairOrTimeframeCommandExecuted, CanChangePairOrTimeframeCommandExecute);
            RunWebSocketCommand = new LambdaCommand(OnRunWebSocketCommandExecuted, CanRunWebSocketCommandExecute);
            #endregion

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Candles = Bybit.GetCandles("BTCUSDT", "1");
        }


        #region NewMessage
        public void OnGetNewMessage(string message)
        {
            string timeframe = null, pair = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                timeframe = SelectedTimeframe.Content.ToString();
                pair = SelectedPair.Content.ToString();
            });
       
            if (message.Contains($"\"topic\":\"candle.{timeframe}.{pair}\""))
            {
                Console.WriteLine("New Message: " + message);
                LinearKlineWebSocketBase klineWebSocketBase = JsonConvert.DeserializeObject<LinearKlineWebSocketBase>(message);
                List<LinearKlineWebSocket> klineWebSocket = klineWebSocketBase.Data;

                if (klineWebSocket[0].Confirm)
                {
                    if (_timestampOpen == klineWebSocket[1].Start)
                        return;
                    else
                    {
                        _timestampOpen = klineWebSocketBase.Data[1].Start;
                        Console.WriteLine($"timestampOpen изменен на ({_timestampOpen})");
                        OnChangeLastCandle(klineWebSocket[0]);
                        OnAddNewCandle(klineWebSocket[1]);
                    }
                }
                else
                {
                    OnChangeLastCandle(klineWebSocket[0]);
                }
            }
        }

        public void OnChangeLastCandle(LinearKlineWebSocket klineV2)
        {
            ICandle candle = Bybit.GetCandleFromLinearKlineWebSocket(klineV2);
            int lastCandleCount = Candles.Count - 1;

            App.Current.Dispatcher.Invoke(() =>
            {
                Candles[lastCandleCount] = candle;
                Console.WriteLine("Изменено: " + candle);
            });
        }

        public void OnAddNewCandle(LinearKlineWebSocket klineV2)
        {
            ICandle candle = Bybit.GetCandleFromLinearKlineWebSocket(klineV2);

            App.Current.Dispatcher.Invoke(() =>
            {
                Candles.Add(candle);
                Console.WriteLine("Добавлено: " + candle);
            });
        }
        #endregion

    }
}
