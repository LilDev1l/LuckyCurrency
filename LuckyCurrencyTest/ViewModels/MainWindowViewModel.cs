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
using LuckyCurrencyTest.Services.Models.OrderBook;
using LuckyCurrencyTest.Models;
using LuckyCurrencyTest.Services.Models.OrderBook.OrderBookSnapshot;
using LuckyCurrencyTest.Services.Models.OrderBook.OrderBookDelta;
using Newtonsoft.Json.Linq;
using System.Linq;
using LuckyCurrencyTest.Services.Models.LastTrade;

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

        #region Список заявок на продажу
        private ObservableCollection<OrderBook> _asks;
        public ObservableCollection<OrderBook> Asks
        {
            get => _asks;
            set => Set(ref _asks, value);
        }
        #endregion

        #region Список заявок на покупку
        private ObservableCollection<OrderBook> _bids;
        public ObservableCollection<OrderBook> Bids
        {
            get => _bids;
            set => Set(ref _bids, value);
        }
        #endregion

        #region Список последних сделок на бирже
        private ObservableCollection<LastTrade> _lastTrades;
        public ObservableCollection<LastTrade> LastTrades
        {
            get => _lastTrades;
            set => Set(ref _lastTrades, value);
        }
        #endregion

        #region Команды

        #region ChangePairOrTimeframeCommand
        public ICommand ChangePairOrTimeframeCommand { get; }
        private bool CanChangePairOrTimeframeCommandExecute(object p) => true;
        private void OnChangePairOrTimeframeCommandExecuted(object p)
        {
            Bybit.ReconnectWebSocket();
            Asks.Clear();
            Bids.Clear();
            LastTrades.Clear();
            Bybit.SendMessage($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe.Content}.{SelectedPair.Content}\"]}}");
            Bybit.SendMessage($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{SelectedPair.Content}\"]}}");
            Bybit.SendMessage($"{{\"op\":\"subscribe\",\"args\":[\"trade.{SelectedPair.Content}\"]}}");
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
            Asks = new ObservableCollection<OrderBook>();
            Bids = new ObservableCollection<OrderBook>();
            LastTrades = new ObservableCollection<LastTrade>();
        }

        #region NewMessage
        private void OnGetNewMessage(string message)
        {
            string timeframe = null, pair = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                timeframe = SelectedTimeframe.Content.ToString();
                pair = SelectedPair.Content.ToString();
            });

            if (message.Contains($"\"topic\":\"candle.{timeframe}.{pair}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewCandle(message);
                });
            }
            if (message.Contains($"\"topic\":\"orderBookL2_25.{pair}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewOrderBook(message);
                });
            }
            if (message.Contains($"\"topic\":\"trade.{pair}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewLastTrade(message);
                });
            }
        }
        private void NewCandle(string message)
        {
            Console.WriteLine("New Message: " + message);
            LinearKlineWebSocketBase klineWebSocketBase = JsonConvert.DeserializeObject<LinearKlineWebSocketBase>(message);
            List<LinearKlineWebSocketData> klineWebSocket = klineWebSocketBase.Data;

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

            void OnChangeLastCandle(LinearKlineWebSocketData klineV2)
            {
                ICandle candle = Bybit.GetCandleFromLinearKlineWebSocket(klineV2);
                int lastCandleCount = Candles.Count - 1;
/*
                App.Current.Dispatcher.Invoke(() =>
                {*/
                    Candles[lastCandleCount] = candle;
                    Console.WriteLine("Изменено: " + candle);
/*                });*/
            }
            void OnAddNewCandle(LinearKlineWebSocketData klineV2)
            {
                ICandle candle = Bybit.GetCandleFromLinearKlineWebSocket(klineV2);

/*                App.Current.Dispatcher.Invoke(() =>
                {*/
                    Candles.Add(candle);
                    Console.WriteLine("Добавлено: " + candle);
/*                });*/
            }
        }
        private void NewOrderBook(string message)
        {
            Console.WriteLine("New Message: " + message);
            OrderBookBase orderBookBase = JsonConvert.DeserializeObject<OrderBookBase>(message);
            if (orderBookBase.Type.Equals("snapshot"))
            {
                OrderBookSnapshot orderBookSnapshots = ((JObject)orderBookBase.Data).ToObject<OrderBookSnapshot>();
                List<OrderBookData> orderBook2 = orderBookSnapshots.Order_book;
                foreach (var obs in orderBook2)
                {
                    if (obs.Side.Equals("Sell"))
                    {
                        Asks.Add(new OrderBook(obs.Id, double.Parse(obs.Price), obs.Size));
                    }
                    else
                    {
                        Bids.Insert(0, new OrderBook(obs.Id, double.Parse(obs.Price), obs.Size));
                    }
                }
            }
            else
            {
                OrderBookDelta orderBookDelta = ((JObject)orderBookBase.Data).ToObject<OrderBookDelta>();

                List<Delete> deletes = orderBookDelta.Delete;
                foreach (var del in deletes)
                {
                    if (del.Side.Equals("Sell"))
                    {
                        Asks.Remove(Asks.First(ask => ask.Id == del.Id));
                    }
                    else
                    {
                        Bids.Remove(Bids.First(ask => ask.Id == del.Id));
                    }
                }

                List<Update> updates = orderBookDelta.Update;
                foreach (var upd in updates)
                {
                    if (upd.Side.Equals("Sell"))
                    {
                        Asks[Asks.IndexOf(Asks.First(ask => ask.Id == upd.Id))] = new OrderBook(upd.Id, double.Parse(upd.Price), upd.Size);
                    }
                    else
                    {
                        Bids[Bids.IndexOf(Bids.First(ask => ask.Id == upd.Id))] = new OrderBook(upd.Id, double.Parse(upd.Price), upd.Size);
                    }
                }

                List<Insert> inserts = orderBookDelta.Insert;
                foreach (var ins in inserts)
                {
                    if (ins.Side.Equals("Sell"))
                    {
                        if(Convert.ToBoolean(Asks.Count))
                        {
                            if (Asks[Asks.Count - 1].Id < ins.Id)
                            {
                                Asks.Add(new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                            }
                            else
                            {
                                Asks.Insert(Asks.IndexOf(Asks.First(ask => ask.Id > ins.Id)), new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                            }
                        }
                        else
                        {
                            Asks.Add(new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                        }
                    }
                    else
                    {
                        if (Convert.ToBoolean(Bids.Count))
                        {
                            if (Bids[Bids.Count - 1].Id > ins.Id)
                            {
                                Bids.Add(new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                            }
                            else
                            {
                                Bids.Insert(Bids.IndexOf(Bids.First(bid => bid.Id < ins.Id)), new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                            }
                        }
                        else
                        {
                            Bids.Add(new OrderBook(ins.Id, double.Parse(ins.Price), ins.Size));
                        }
                    }
                }
            }
        }
        private void NewLastTrade(string message)
        {
            Console.WriteLine("New Message: " + message);
            LastTradeBase lastTradeBase = JsonConvert.DeserializeObject<LastTradeBase>(message);
            List<LastTradeData> lastTrades = lastTradeBase.Data;

            foreach(var lastTrade in lastTrades) 
            {
                LastTrades.Insert(0, new LastTrade(lastTrade.Price, lastTrade.Size, lastTrade.Timestamp, lastTrade.Tick_direction, lastTrade.Side));

                if (LastTrades.Count > 25)
                {
                    LastTrades.RemoveAt(LastTrades.Count - 1);
                }
            }
        }
        #endregion
    }
}
