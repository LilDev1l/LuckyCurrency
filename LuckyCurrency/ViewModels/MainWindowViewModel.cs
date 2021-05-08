using LuckyCurrency.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FancyCandles;
using LuckyCurrency.Services;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Input;
using LuckyCurrency.Infrastructure.Commands;
using LuckyCurrency.Services.Models.LinearKlineWS;
using LuckyCurrency.Services.Models.OrderBook;
using LuckyCurrency.Models;
using LuckyCurrency.Services.Models.OrderBook.OrderBookSnapshot;
using LuckyCurrency.Services.Models.OrderBook.OrderBookDelta;
using Newtonsoft.Json.Linq;
using System.Linq;
using LuckyCurrency.Services.Models.LastTrade;
using LuckyCurrency.Services.Models.Balance;
using LuckyCurrency.Services.Models.BalanceWebSocket;
using LuckyCurrency.Services.Models.LinearKline;
using LuckyCurrency.Services.Models.Position;
using LuckyCurrency.Services.Models.OrderWS;
using LuckyCurrency.Services.Models.Order;

namespace LuckyCurrency.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Models

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
        private ObservableCollection<OrderBook> _asks = new ObservableCollection<OrderBook>();
        public ObservableCollection<OrderBook> Asks
        {
            get => _asks;
            set => Set(ref _asks, value);
        }
        #endregion

        #region Список заявок на покупку
        private ObservableCollection<OrderBook> _bids = new ObservableCollection<OrderBook>();
        public ObservableCollection<OrderBook> Bids
        {
            get => _bids;
            set => Set(ref _bids, value);
        }
        #endregion

        #region Список последних сделок на бирже
        private ObservableCollection<Trade> _trades = new ObservableCollection<Trade>();
        public ObservableCollection<Trade> Trades
        {
            get => _trades;
            set => Set(ref _trades, value);
        }
        #endregion

        #region Текущий баланс
        private Balance _balance;
        public Balance Balance
        {
            get => _balance;
            set => Set(ref _balance, value);
        }
        #endregion

        #region Позиции
        private ObservableCollection<Position> _positions;
        public ObservableCollection<Position> Positions
        {
            get => _positions;
            set => Set(ref _positions, value);
        }
        #endregion

        #region Ордера
        private ObservableCollection<Order> _orders;
        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set => Set(ref _orders, value);
        }
        #endregion

        #endregion

        #region Команды

        #region ChangePairOrTimeframeCommand
        public ICommand ChangePairOrTimeframeCommand { get; }
        private bool CanChangePairOrTimeframeCommandExecute(object p) => true;
        private void OnChangePairOrTimeframeCommandExecuted(object p)
        {
            Bybit.ReconnectPublicWS();
            Asks.Clear();
            Bids.Clear();
            Trades.Clear();

            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe.Content}.{SelectedPair.Content}\"]}}");
            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{SelectedPair.Content}\"]}}");
            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{SelectedPair.Content}\"]}}");

            Candles = GetCandles(SelectedPair.Content.ToString(), SelectedTimeframe.Content.ToString());
            Positions = GetPositions(SelectedPair.Content.ToString());
            Orders = GetOrders(SelectedPair.Content.ToString(), "New");
        }
        #endregion

        #region RunWSCommand
        public ICommand RunWSCommand { get; }
        private bool CanRunWebSocketCommandExecute(object p) => true;
        private void OnRunWebSocketCommandExecuted(object p)
        {
            Bybit.NewMessage += GetNewMessage;
            Bybit.RunBybitWS();
/*
            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe.Content}.{SelectedPair.Content}\"]}}");
            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{SelectedPair.Content}\"]}}");
            Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{SelectedPair.Content}\"]}}");

            Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"wallet\"]}");*/
            Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"position\"]}");
            //Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"order\"]}");

            Balance = GetBalance("USDT");
            Positions = GetPositions(SelectedPair.Content.ToString());
            Orders = GetOrders(SelectedPair.Content.ToString(), "New");
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды
            ChangePairOrTimeframeCommand = new LambdaCommand(OnChangePairOrTimeframeCommandExecuted, CanChangePairOrTimeframeCommandExecute);
            RunWSCommand = new LambdaCommand(OnRunWebSocketCommandExecuted, CanRunWebSocketCommandExecute);
            #endregion

            Candles = GetCandles("BTCUSDT", "15");
        }

        #region HTTP

        #region public
        private static ObservableCollection<ICandle> GetCandles(string pair, string timeframe)
        {
            ICandle GetCandleFromLinearKline(LinearKlineData kline)
            {
                DateTime openTime = new DateTime(kline.OpenTime * 10000000L + Bybit.Duration);

                return new Candle(openTime, kline.Open, kline.High, kline.Low, kline.Close, (long)kline.Volume);
            }

            LinearKlineBase linearKlineBase = Bybit.GetLinearKlineBase(pair, timeframe);
            List<LinearKlineData> linearKlineData = linearKlineBase.Result;

            ObservableCollection<ICandle> candles = new ObservableCollection<ICandle>();
            foreach (var kline in linearKlineData)
            {
                candles.Add(GetCandleFromLinearKline(kline));
            }

            return candles;
        }
        #endregion

        #region private
        private static Balance GetBalance(string coin)
        {
            BalanceBase currentBalanceBase = Bybit.GetBalanceBase(coin);
            BalanceData currentBalanceData = currentBalanceBase.Result.USDT;

            return new Balance(currentBalanceData.Wallet_balance, currentBalanceData.Available_balance);
        }
        private static ObservableCollection<Position> GetPositions(string symbol)
        {
            PositionBase positionBase = Bybit.GetPositionBase(symbol);
            List<PositionData> positionData = positionBase.result;

            ObservableCollection<Position> positions = new ObservableCollection<Position>();
            foreach (var pos in positionData)
            {
                positions.Add(new Position(pos.symbol, pos.side, pos.size, pos.position_value, pos.entry_price, pos.liq_price, pos.position_margin, pos.realised_pnl));
            }

            return positions;
        }
        private static ObservableCollection<Order> GetOrders(string symbol, string orderStatus = null)
        {
            OrderBase orderBase = Bybit.GetOrderBase(symbol, orderStatus);
            OrderPage orderPage = orderBase.result;
            List<OrderData> orderDatas = orderPage.data;

            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            if (orderDatas != null)
            {
                foreach (var order in orderDatas)
                {
                    orders.Add(new Order(order.order_id, order.symbol, order.side, order.order_type, order.price, order.qty, order.order_status, order.take_profit, order.stop_loss, order.created_time));
                }
            }

            return orders;
        }
        #endregion

        #endregion

        #region WebSockets

        private void GetNewMessage(string message)
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
                    NewTrade(message);
                });
            }
            if (message.Contains($"\"topic\":\"wallet\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewCurrentBalance(message);
                });
            }
            if (message.Contains($"\"topic\":\"position\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewPosition(message);
                });
            }
            if (message.Contains($"\"topic\":\"order\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewOrder(message);
                });
            }
        }

        #region public
        private void NewCandle(string message)
        {
            Console.WriteLine("New Message: " + message);
            LinearKlineWSBase klineWebSocketBase = JsonConvert.DeserializeObject<LinearKlineWSBase>(message);
            List<LinearKlineWSData> klineWebSocket = klineWebSocketBase.Data;

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

            void OnChangeLastCandle(LinearKlineWSData klineV2)
            {
                ICandle candle = GetCandleFromLinearKlineWebSocket(klineV2);
                int lastCandleCount = Candles.Count - 1;

                Candles[lastCandleCount] = candle;
                Console.WriteLine("Изменено: " + candle);
            }
            void OnAddNewCandle(LinearKlineWSData klineV2)
            {
                ICandle candle = GetCandleFromLinearKlineWebSocket(klineV2);

                Candles.Add(candle);
                Console.WriteLine("Добавлено: " + candle);
            }

            ICandle GetCandleFromLinearKlineWebSocket(LinearKlineWSData klineWS)
            {
                DateTime openTime = new DateTime(klineWS.Start * 10000000L + Bybit.Duration);

                return new Candle(openTime, klineWS.Open, klineWS.High, klineWS.Low, klineWS.Close, (int)double.Parse(klineWS.Volume));
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
        private void NewTrade(string message)
        {
            Console.WriteLine("New Message: " + message);
            LastTradeBase lastTradeBase = JsonConvert.DeserializeObject<LastTradeBase>(message);
            List<LastTradeData> lastTrades = lastTradeBase.Data;

            foreach(var lastTrade in lastTrades) 
            {
                Trades.Insert(0, new Trade(lastTrade.Price, lastTrade.Size, lastTrade.Timestamp, lastTrade.Tick_direction, lastTrade.Side));

                if (Trades.Count > 25)
                {
                    Trades.RemoveAt(Trades.Count - 1);
                }
            }
        }
        #endregion

        #region private
        private void NewCurrentBalance(string message)
        {
            Console.WriteLine("New Message: " + message);
            BalanceWSBase currentBalanceWSBase = JsonConvert.DeserializeObject<BalanceWSBase>(message);
            BalanceWSData currentBalanceWSData  = currentBalanceWSBase.Data[0];

            Balance = new Balance(currentBalanceWSData.Wallet_balance, currentBalanceWSData.Available_balance);
        }
        private void NewPosition(string message)
        {
            Console.WriteLine("New Message: " + message);
            PositionWSBase positionBase = JsonConvert.DeserializeObject<PositionWSBase>(message);
            List<PositionWSData> positionData = positionBase.Data;

            foreach (var pos in positionData)
            {
                if (pos.Symbol == SelectedPair.Content.ToString())
                {
                    Positions[Positions.IndexOf(Positions.First(p => p.Side == pos.Side))] = new Position(pos.Symbol, pos.Side, pos.Size, pos.Position_value, pos.Entry_price, pos.Liq_price, pos.Position_margin, pos.Realised_pnl);
                }
            }
        }
        private void NewOrder(string message)
        {
            Console.WriteLine("New Message: " + message);
            OrderWSBase orderWSBase = JsonConvert.DeserializeObject<OrderWSBase>(message);
            List<OrderWSData> orderWSDatas = orderWSBase.data;

            foreach(var order in orderWSDatas)
            {
                if (order.symbol == SelectedPair.Content.ToString())
                {
                    if (order.order_status == "New")
                        Orders.Add(new Order(order.order_id, order.symbol, order.side, order.order_type, order.price, order.qty, order.order_status, order.take_profit, order.stop_loss, order.create_time));
                    if (order.order_status == "Cancelled")
                    {
                        Orders?.Remove(Orders.FirstOrDefault(ord => ord.Order_id == order.order_id));
                    }
                }
            }
        }
        #endregion

        #endregion
    }
}
