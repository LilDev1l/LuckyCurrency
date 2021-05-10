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
using LuckyCurrency.Services.Models.Symbol;
using LuckyCurrency.Services.Models.PositionClosePnl;
using System.Threading;
using System.Threading.Tasks;

namespace LuckyCurrency.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Models

        #region Торговая пара
        private ComboBoxItem _selectedPair;
        public ComboBoxItem SelectedSymbol
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

        #region Symbols
        private List<SymbolData> _symbols;
        public List<SymbolData> Symbols
        {
            get => _symbols;
            set => Set(ref _symbols, value);
        }
        #endregion

        #region Price Order
        private double _priceOrder;
        public double PriceOrder
        {
            get => _priceOrder;
            set => Set(ref _priceOrder, value);
        }
        #endregion

        #region Qty Order
        private double _qty;
        public double QtyOrder
        {
            get => _qty;
            set => Set(ref _qty, value);
        }
        #endregion

        #region Positions Close Pnl
        private List<PositionClosePnlData> _positionsClosePnl;
        public List<PositionClosePnlData> PositionsClosePnl
        {
            get => _positionsClosePnl;
            set => Set(ref _positionsClosePnl, value);
        }
        #endregion

        #region Flag
        private bool _flag;
        public bool Flag
        {
            get => _flag;
            set => Set(ref _flag, value);
        }
        #endregion

        #endregion

        #region Команды

        #region ChangeSymbolCommand
        public ICommand ChangeSymbolCommand { get; }
        private bool CanChangeSymbolCommandExecute(object p) => Flag;
        private void OnChangeSymbolCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                Bybit.ReconnectPublicWS();

                string timeframe = null, symbol = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    timeframe = SelectedTimeframe.Content.ToString();
                    symbol = SelectedSymbol.Content.ToString();

                    
                    Bids.Clear();
                    Trades.Clear();
                    Asks.Clear();
                });
                
                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{timeframe}.{symbol}\"]}}");
                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{symbol}\"]}}");
                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{symbol}\"]}}");

                Candles = GetCandles(symbol, timeframe);
                Positions = GetPositions(symbol);
                Orders = GetOrders(symbol, "New");
                PositionsClosePnl = Bybit.GetPositionsClosePnl(symbol).result.data;
            });
        }
        #endregion
        #region ChangeTimeframeCommand
        public ICommand ChangeTimeframeCommand { get; }
        private bool CanChangeTimeframeCommandExecute(object p) => Flag;
        private void OnChangeTimeframeCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                string timeframe = null, symbol = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    timeframe = SelectedTimeframe.Content.ToString();
                    symbol = SelectedSymbol.Content.ToString();
                });

                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{timeframe}.{symbol}\"]}}");
                Candles = GetCandles(symbol, timeframe);
            });
        }
        #endregion

        #region CreateOpenLimitOrderCommand
        public ICommand CreateOpenLimitOrderCommand { get; }
        private bool CanCreateOpenLimitOrderCommandExecute(object p) => true;
        private void OnCreateOpenLimitOrderCommandExecuted(object p)
        {
            if (p is string side)
            {
                Bybit.CreateLimitOrder(side, SelectedSymbol.Content.ToString(), QtyOrder, PriceOrder, "PostOnly", false, false);
            }
        }
        #endregion
        #region CreateOpenMarketOrderCommand
        public ICommand CreateOpenMarketOrderCommand { get; }
        private bool CanCreateOpenMarketOrderCommandExecute(object p) => true;
        private void OnCreateOpenMarketOrderCommandExecuted(object p)
        {
            if (p is string side)
            {
                Bybit.CreateMarketOrder(side, SelectedSymbol.Content.ToString(), QtyOrder, "ImmediateOrCancel", false, false);
            }
        }
        #endregion
        #region CreateCloseLimitOrderCommand
        public ICommand CreateCloseLimitOrderCommand { get; }
        private bool CanCreateCloseLimitOrderCommandExecute(object p) => true;
        private void OnCreateCloseLimitOrderCommandExecuted(object p)
        {
            if (p is string side)
            {
                Bybit.CreateLimitOrder(side, SelectedSymbol.Content.ToString(), QtyOrder, PriceOrder, "PostOnly", true, true);
            }
        }
        #endregion
        #region CreateCloseMarketOrderCommand
        public ICommand CreateCloseMarketOrderCommand { get; }
        private bool CanCreateCloseMarketOrderCommandExecute(object p) => true;
        private void OnCreateCloseMarketOrderCommandExecuted(object p)
        {
            if (p is string side)
            {
                Bybit.CreateMarketOrder(side, SelectedSymbol.Content.ToString(), QtyOrder, "ImmediateOrCancel", true, true);
            }
        }
        #endregion

        #region RunWSCommand
        public ICommand RunWSCommand { get; }
        private bool CanRunWebSocketCommandExecute(object p) => true;
        private void OnRunWebSocketCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                string timeframe = null, symbol = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    timeframe = SelectedTimeframe.Content.ToString();
                    symbol = SelectedSymbol.Content.ToString();
                });

                Candles = GetCandles(symbol, timeframe);
                Symbols = Bybit.GetSymbolBase().result;
                Balance = GetBalance("USDT");
                Positions = GetPositions(symbol);
                Orders = GetOrders(symbol, "New");
                PositionsClosePnl = Bybit.GetPositionsClosePnl(symbol).result.data;

                Bybit.NewMessage += GetNewMessage;

                Bybit.RunBybitWS();

                Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"wallet\"]}");
                Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"position\"]}");
                Bybit.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"order\"]}");

                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{timeframe}.{symbol}\"]}}");
                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{symbol}\"]}}");
                Bybit.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{symbol}\"]}}");

                Flag = true;
            });
        }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Команды
            ChangeSymbolCommand = new LambdaCommand(OnChangeSymbolCommandExecuted, CanChangeSymbolCommandExecute);
            ChangeTimeframeCommand = new LambdaCommand(OnChangeTimeframeCommandExecuted, CanChangeTimeframeCommandExecute);
            RunWSCommand = new LambdaCommand(OnRunWebSocketCommandExecuted, CanRunWebSocketCommandExecute);

            CreateOpenLimitOrderCommand = new LambdaCommand(OnCreateOpenLimitOrderCommandExecuted, CanCreateOpenLimitOrderCommandExecute);
            CreateOpenMarketOrderCommand = new LambdaCommand(OnCreateOpenMarketOrderCommandExecuted, CanCreateOpenMarketOrderCommandExecute);

            CreateCloseLimitOrderCommand = new LambdaCommand(OnCreateCloseLimitOrderCommandExecuted, CanCreateCloseLimitOrderCommandExecute);
            CreateCloseMarketOrderCommand = new LambdaCommand(OnCreateCloseMarketOrderCommandExecuted, CanCreateCloseMarketOrderCommandExecute);
            #endregion

            Bybit.SetCultureUS(); 
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
            //Console.WriteLine(message);
            string timeframe = null, pair = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                timeframe = SelectedTimeframe.Content.ToString();
                pair = SelectedSymbol.Content.ToString();
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
            //Console.WriteLine("New Message: " + message);
            LinearKlineWSBase klineWebSocketBase = JsonConvert.DeserializeObject<LinearKlineWSBase>(message);
            List<LinearKlineWSData> klineWebSocket = klineWebSocketBase.Data;

            if (klineWebSocket[0].Confirm)
            {
                if (_timestampOpen == klineWebSocket[1].Start)
                    return;
                else
                {
                    _timestampOpen = klineWebSocketBase.Data[1].Start;
                    //Console.WriteLine($"timestampOpen изменен на ({_timestampOpen})");
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
                //Console.WriteLine("Изменено: " + candle);
            }
            void OnAddNewCandle(LinearKlineWSData klineV2)
            {
                ICandle candle = GetCandleFromLinearKlineWebSocket(klineV2);

                Candles.Add(candle);
                //Console.WriteLine("Добавлено: " + candle);
            }

            ICandle GetCandleFromLinearKlineWebSocket(LinearKlineWSData klineWS)
            {
                DateTime openTime = new DateTime(klineWS.Start * 10000000L + Bybit.Duration);

                return new Candle(openTime, klineWS.Open, klineWS.High, klineWS.Low, klineWS.Close, (int)double.Parse(klineWS.Volume));
            }
        }
        private void NewOrderBook(string message)
        {
            //Console.WriteLine("New Message: " + message);
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
            //Console.WriteLine("New Message: " + message);
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
            //Console.WriteLine("New Message: " + message);
            BalanceWSBase currentBalanceWSBase = JsonConvert.DeserializeObject<BalanceWSBase>(message);
            BalanceWSData currentBalanceWSData  = currentBalanceWSBase.Data[0];

            Balance = new Balance(currentBalanceWSData.Wallet_balance, currentBalanceWSData.Available_balance);
        }
        private void NewPosition(string message)
        {
            //Console.WriteLine("New Message: " + message);
            PositionWSBase positionBase = JsonConvert.DeserializeObject<PositionWSBase>(message);
            List<PositionWSData> positionData = positionBase.Data;

            foreach (var pos in positionData)
            {
                if (pos.Symbol == SelectedSymbol.Content.ToString())
                {
                    Positions[Positions.IndexOf(Positions.First(p => p.Side == pos.Side))] = new Position(pos.Symbol, pos.Side, pos.Size, pos.Position_value, pos.Entry_price, pos.Liq_price, pos.Position_margin, pos.Realised_pnl);
                }
            }
        }
        private void NewOrder(string message)
        {
            //Console.WriteLine("New Message: " + message);
            OrderWSBase orderWSBase = JsonConvert.DeserializeObject<OrderWSBase>(message);
            List<OrderWSData> orderWSDatas = orderWSBase.data;

            foreach(var order in orderWSDatas)
            {
                if (order.symbol == SelectedSymbol.Content.ToString())
                {
                    if (order.order_status == "New")
                        Orders.Insert(0, new Order(order.order_id, order.symbol, order.side, order.order_type, order.price, order.qty, order.order_status, order.take_profit, order.stop_loss, order.create_time));
                    if (order.order_status == "Cancelled" || order.order_status == "Filled")
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
