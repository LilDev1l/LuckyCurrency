#region Using
using LuckyCurrency.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FancyCandles;
using Newtonsoft.Json;
using System.Windows.Controls;
using System.Windows.Input;
using LuckyCurrency.Infrastructure.Commands;
using LuckyCurrency.Models;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows;
using LuckyCurrency.Views.Authorization;
using ControlzEx.Theming;
using Bybit.Model.Symbol;
using Bybit;
using Bybit.Model.Balance;
using Bybit.Model.Position2;
using Bybit.Model.Order;
using Bybit.Model.PositionClosePnl;
using Bybit.Model.BalanceWebSocket;
using Bybit.Model.OrderBookDelta;
using Bybit.Model.LastTrade;
using Bybit.Model.LinearKline;
using Bybit.Model.LinearKlineWS;
using Bybit.Model.OrderBook;
using Bybit.Model.OrderBookSnapshot;
using Bybit.Model.PositionWS;
using Bybit.Model.OrderWS;
using LuckyCurrency.Data.Model;
#endregion

namespace LuckyCurrency.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Models

        #region Title
        private string _title = "LuckyCurrency";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region CurrentTheme
        private string _currentTheme = "Light";
        public string CurrentTheme
        {
            get => _currentTheme;
            set => Set(ref _currentTheme, value);
        }
        #endregion

        #region ForegroundColorTheme
        private string _fontColorTheme = "Black";
        public string ForegroundColorTheme
        {
            get => _fontColorTheme;
            set => Set(ref _fontColorTheme, value);
        }
        #endregion

        #region BackgroundColorTheme
        private string _backgroundColorTheme = "White";
        public string BackgroundColorTheme
        {
            get => _backgroundColorTheme;
            set => Set(ref _backgroundColorTheme, value);
        }
        #endregion

        #region Уведомления
        public Notifier Notifier { get; set; } = new Notifier(cfg =>
        {
            Window window = null;
            WindowCollection windowCollection = null;

            App.Current.Dispatcher.Invoke(() =>
            {
                windowCollection = App.Current.Windows;
            });

            foreach (var win in windowCollection)
            {
                if (win is MainWindow main)
                    window = main;
            }

            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: window,
            corner: Corner.BottomRight,
            offsetX: 5,
            offsetY: 0);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(3));

            App.Current.Dispatcher.Invoke(() =>
            {
                cfg.Dispatcher = App.Current.Dispatcher;
            });
        });
        #endregion

        #region Таймфрейм
        private string _selectedTimeframe;
        public string SelectedTimeframe
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

        #region Trades
        private ObservableCollection<Trade> _trades = new ObservableCollection<Trade>();
        public ObservableCollection<Trade> Trades
        {
            get => _trades;
            set => Set(ref _trades, value);
        }
        #endregion

        #region LastTrade
        private Trade _trade;
        public Trade LastTrade
        {
            get => _trade;
            set => Set(ref _trade, value);
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

        #region CountOrder
        private int _countOrder;
        public int CountOrder
        {
            get => _countOrder;
            set => Set(ref _countOrder, value);
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
        private List<PositionClosePnl> _positionsClosePnl;
        public List<PositionClosePnl> PositionsClosePnl
        {
            get => _positionsClosePnl;
            set => Set(ref _positionsClosePnl, value);
        }
        #endregion

        #region CurrentSymbol
        private SymbolData _currentSymbol;
        public SymbolData CurrentSymbol
        {
            get => _currentSymbol;
            set => Set(ref _currentSymbol, value);
        }
        #endregion

        #region WsRun
        private bool _wsRun;
        public bool WsRun
        {
            get => _wsRun;
            set => Set(ref _wsRun, value);
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

        #region TimeFrames
        private List<string> _timeFrames = new List<string>() { "1m",
                                                                "15m",
                                                                "30m",
                                                                "1h",
                                                                "4h",
                                                                "D",
                                                                "W",
                                                                "M"};
        public List<string> TimeFrames
        {
            get => _timeFrames;
            set => Set(ref _timeFrames, value);
        }
        #endregion

        #endregion

        #region Команды

        #region LoadedCommand 
        public ICommand LoadedCommand { get; }
        private bool CanLoadedCommandExecute(object p) => true;
        private void OnLoadedCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                Symbols = BybitClient.GetSymbolBase().result.FindAll(s => s.quote_currency == "USDT");

                PriceOrder = CurrentSymbol.price_filter.min_price;
                QtyOrder = CurrentSymbol.lot_size_filter.min_trading_qty;

                Candles = GetCandles(CurrentSymbol.alias, SelectedTimeframe);
                BybitClient.NewMessage += GetNewMessage;
                BybitClient.RunBybitWS();
                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe}.{CurrentSymbol.alias}\"]}}");
                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{CurrentSymbol.alias}\"]}}");
                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{CurrentSymbol.alias}\"]}}");

                if (BybitClient.IsTrueAPI_Key())
                {
                    Balance = GetBalance("USDT");
                    Positions = GetPositions(CurrentSymbol.alias);
                    Orders = GetOrders(CurrentSymbol.alias, "New");
                    PositionsClosePnl = GetPositionsClosePnl(CurrentSymbol.alias, "Trade");

                    BybitClient.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"wallet\"]}");
                    BybitClient.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"position\"]}");
                    BybitClient.SendPrivateWS("{\"op\":\"subscribe\",\"args\":[\"order\"]}");
                }
                else
                {
                    Notifier.ShowError($"Invalid api_key");
                }

                WsRun = true;
            });
        }
        #endregion

        #region ChangeSymbolCommand
        public ICommand ChangeSymbolCommand { get; }
        private bool CanChangeSymbolCommandExecute(object p) => WsRun;
        private void OnChangeSymbolCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                BybitClient.ReconnectPublicWS();

                App.Current.Dispatcher.Invoke(() =>
                {
                    Bids.Clear();
                    Trades.Clear();
                    Asks.Clear();
                });

                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe}.{CurrentSymbol.alias}\"]}}");
                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"orderBookL2_25.{CurrentSymbol.alias}\"]}}");
                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"trade.{CurrentSymbol.alias}\"]}}");

                PriceOrder = CurrentSymbol.price_filter.min_price;
                QtyOrder = CurrentSymbol.lot_size_filter.min_trading_qty;
                Candles = GetCandles(CurrentSymbol.alias, SelectedTimeframe);
                Positions = GetPositions(CurrentSymbol.alias);
                Orders = GetOrders(CurrentSymbol.alias, "New");
                PositionsClosePnl = GetPositionsClosePnl(CurrentSymbol.alias, "Trade");
            });
        }
        #endregion
        #region ChangeTimeframeCommand
        public ICommand ChangeTimeframeCommand { get; }
        private bool CanChangeTimeframeCommandExecute(object p) => WsRun;
        private void OnChangeTimeframeCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                Candles = GetCandles(CurrentSymbol.alias, SelectedTimeframe);

                BybitClient.SendPublicWS($"{{\"op\":\"subscribe\",\"args\":[\"candle.{SelectedTimeframe}.{CurrentSymbol.alias}\"]}}");
            });
        }
        #endregion
        #region SelectedPriceCommand
        public ICommand SelectedPriceCommand { get; }
        private bool CanSelectedPriceCommandExecute(object p)
        {
            if (p is DataGrid d && d.SelectedItem != null)
                return true;
            else
                return false;
        }
        private void OnSelectedPriceCommandExecuted(object p)
        {
            if (p is DataGrid d)
            {
                PriceOrder = ((OrderBook)d.SelectedItem).Price;
                d.UnselectAll();
            }
        }
        #endregion
        #region CreateOpenLimitOrderCommand
        public ICommand CreateOpenLimitOrderCommand { get; }
        private bool CanCreateOpenLimitOrderCommandExecute(object p) => true;
        private void OnCreateOpenLimitOrderCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is string side)
                {
                    OrderBase orderBase = BybitClient.CreateLimitOrder(side, CurrentSymbol.alias, QtyOrder, PriceOrder, "PostOnly", false, false);
                    if (orderBase.ret_code == 0)
                    {
                        OrderData orderData = ((JObject)orderBase.result).ToObject<OrderData>();
                        if (orderData.side == "Buy")
                            Notifier.ShowSuccess($"Order Submitted Successfully\n\n{orderData.qty} {CurrentSymbol.base_currency} contracts will be bought at {orderData.price} price.");
                        else
                            Notifier.ShowSuccess($"Order Submitted Successfully\n\n{orderData.qty} {CurrentSymbol.base_currency} contracts will be sold at {orderData.price} price.");
                    }
                    else
                    {
                        if (orderBase.ret_code == 130021)
                        {
                            Notifier.ShowError($"Order submission failed\n\norder cost not available");
                        }
                        else
                        {
                            Notifier.ShowError($"Order submission failed\n\n{orderBase.ret_msg}");
                        }
                    }
                }
            });
        }
        #endregion
        #region CreateOpenMarketOrderCommand
        public ICommand CreateOpenMarketOrderCommand { get; }
        private bool CanCreateOpenMarketOrderCommandExecute(object p) => true;
        private void OnCreateOpenMarketOrderCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is string side)
                {
                    OrderBase orderBase = BybitClient.CreateMarketOrder(side, CurrentSymbol.alias, QtyOrder, "ImmediateOrCancel", false, false);
                    if (orderBase.ret_code == 0)
                    {
                        OrderData orderData = ((JObject)orderBase.result).ToObject<OrderData>();
                        if (orderData.side == "Buy")
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nBought {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                        else
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nSold {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                    }
                    else
                    {
                        Notifier.ShowError($"Order submission failed\n\n{orderBase.ret_msg}");
                    }
                }
            });
        }
        #endregion
        #region CreateCloseLimitOrderCommand
        public ICommand CreateCloseLimitOrderCommand { get; }
        private bool CanCreateCloseLimitOrderCommandExecute(object p) => true;
        private void OnCreateCloseLimitOrderCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is string side)
                {
                    OrderBase orderBase = BybitClient.CreateLimitOrder(side, CurrentSymbol.alias, QtyOrder, PriceOrder, "PostOnly", true, true);
                    if (orderBase.ret_code == 0)
                    {
                        OrderData orderData = ((JObject)orderBase.result).ToObject<OrderData>();
                        if (orderData.side == "Buy")
                            Notifier.ShowSuccess($"Order Submitted Successfully\n\n{orderData.qty} {CurrentSymbol.base_currency} contracts will be bought at {orderData.price} price.");
                        else
                            Notifier.ShowSuccess($"Order Submitted Successfully\n\n{orderData.qty} {CurrentSymbol.base_currency} contracts will be sold at {orderData.price} price.");
                    }
                    else
                    {
                        Notifier.ShowError($"Order submission failed\n\n{orderBase.ret_msg}");
                    }
                }
            });
        }
        #endregion
        #region CreateCloseMarketOrderCommand
        public ICommand CreateCloseMarketOrderCommand { get; }
        private bool CanCreateCloseMarketOrderCommandExecute(object p) => true;
        private void OnCreateCloseMarketOrderCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is string side)
                {
                    OrderBase orderBase = BybitClient.CreateMarketOrder(side, CurrentSymbol.alias, QtyOrder, "ImmediateOrCancel", true, true);
                    if (orderBase.ret_code == 0)
                    {
                        OrderData orderData = ((JObject)orderBase.result).ToObject<OrderData>();
                        if (orderData.side == "Buy")
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nBought {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                        else
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nSold {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                    }
                    else
                    {
                        Notifier.ShowError($"Order submission failed\n\n{orderBase.ret_msg}");
                    }
                }
            });
        }
        #endregion
        #region CancelOrderCommand
        public ICommand CancelOrderCommand { get; }
        private bool CanCancelOrderCommandExecute(object p) => true;
        private void OnCancelOrderCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is Order order)
                {
                    OrderBase orderBase = BybitClient.CancelOrder(order.Symbol, order.Order_id);
                    if (orderBase.ret_code == 0)
                    {
                        Notifier.ShowSuccess($"Cancelled Successfully");
                    }
                    else
                    {
                        Notifier.ShowError($"{orderBase.ret_msg}");
                    }
                }
            });
        }
        #endregion
        #region ClosePositionCommand
        public ICommand ClosePositionCommand { get; }
        private bool CanClosePositionCommandExecute(object p)
        {
            if (p is Position position)
            {
                return position.Size != 0;
            }
            return false;
        }
        private void OnClosePositionCommandExecuted(object p)
        {
            Task.Run(() =>
            {
                if (p is Position position)
                {
                    OrderBase orderBase = BybitClient.CreateMarketOrder(position.Side == "Long" ? "Sell" : "Buy", position.Symbol, position.Size, "GoodTillCancel", true, true);
                    if (orderBase.ret_code == 0)
                    {
                        OrderData orderData = ((JObject)orderBase.result).ToObject<OrderData>();
                        if (orderData.side == "Buy")
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nBought {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                        else
                            Notifier.ShowSuccess($"Your entire order has been failed\n\nSold {orderData.qty} {CurrentSymbol.base_currency} contracts at market price.");
                    }
                    else
                    {
                        Notifier.ShowError($"Order submission failed\n\n{orderBase.ret_msg}");
                    }
                }
            });
        }
        #endregion
        #region ChangeThemeCommand
        public ICommand ChangeThemeCommand { get; }
        private bool CanChangeThemeCommandExecute(object p) => true;
        private void OnChangeThemeCommandExecuted(object p)
        {
            if (CurrentTheme == "Light")
            {
                ThemeManager.Current.ChangeThemeBaseColor(App.Current, "Dark");
                CurrentTheme = "Dark";
                ForegroundColorTheme = "White";
                BackgroundColorTheme = "Black";
            }
            else
            {
                ThemeManager.Current.ChangeThemeBaseColor(App.Current, "Light");
                CurrentTheme = "Light";
                ForegroundColorTheme = "Black";
                BackgroundColorTheme = "White";
            }
        }
        #endregion
        #region SwitchToLoginCommand
        public ICommand SwitchToLoginCommand { get; }
        private bool CanSwitchToLoginCommandExecute(object p) => WsRun;
        private void OnSwitchToLoginCommandExecuted(object p)
        {
            BybitClient.ReconnectPrivateWS();
            BybitClient.ReconnectPublicWS();
            BybitClient.NewMessage -= GetNewMessage;
            SwitchTo(new Login());
        }
        #endregion

        #region ClosingCommand 
        public ICommand ClosingCommand { get; }
        private bool CanClosingCommandExecute(object p) => true;
        private void OnClosingCommandExecuted(object p)
        {
            BybitClient.ReconnectPrivateWS();
            BybitClient.ReconnectPublicWS();
            BybitClient.NewMessage -= GetNewMessage;
        }
        #endregion

        #endregion

        public MainWindowViewModel(API_Key api_key)
        {
            #region Команды
            LoadedCommand = new LambdaCommand(OnLoadedCommandExecuted, CanLoadedCommandExecute);

            ChangeSymbolCommand = new LambdaCommand(OnChangeSymbolCommandExecuted, CanChangeSymbolCommandExecute);
            ChangeTimeframeCommand = new LambdaCommand(OnChangeTimeframeCommandExecuted, CanChangeTimeframeCommandExecute);
            SelectedPriceCommand = new LambdaCommand(OnSelectedPriceCommandExecuted, CanSelectedPriceCommandExecute);
            CreateOpenLimitOrderCommand = new LambdaCommand(OnCreateOpenLimitOrderCommandExecuted, CanCreateOpenLimitOrderCommandExecute);
            CreateOpenMarketOrderCommand = new LambdaCommand(OnCreateOpenMarketOrderCommandExecuted, CanCreateOpenMarketOrderCommandExecute);
            CreateCloseLimitOrderCommand = new LambdaCommand(OnCreateCloseLimitOrderCommandExecuted, CanCreateCloseLimitOrderCommandExecute);
            CreateCloseMarketOrderCommand = new LambdaCommand(OnCreateCloseMarketOrderCommandExecuted, CanCreateCloseMarketOrderCommandExecute);
            CancelOrderCommand = new LambdaCommand(OnCancelOrderCommandExecuted, CanCancelOrderCommandExecute);
            ClosePositionCommand = new LambdaCommand(OnClosePositionCommandExecuted, CanClosePositionCommandExecute);
            ChangeThemeCommand = new LambdaCommand(OnChangeThemeCommandExecuted, CanChangeThemeCommandExecute);
            SwitchToLoginCommand = new LambdaCommand(OnSwitchToLoginCommandExecuted, CanSwitchToLoginCommandExecute);

            ClosingCommand = new LambdaCommand(OnClosingCommandExecuted, CanClosingCommandExecute);
            #endregion

            BybitClient.SetCultureUS();
            BybitClient.SetAPI_Key(api_key.PublicKey);
            BybitClient.SetSecret_Key(api_key.SecretKey);
        }

        #region HTTP

        #region public
        private ObservableCollection<ICandle> GetCandles(string pair, string timeframe)
        {
            ICandle GetCandleFromLinearKline(LinearKlineData kline)
            {
                DateTime openTime = new DateTime(kline.OpenTime * 10000000L + BybitClient.Duration);

                return new Candle(openTime, kline.Open, kline.High, kline.Low, kline.Close, (long)kline.Volume);
            }

            LinearKlineBase linearKlineBase = BybitClient.GetLinearKlineBase(pair, timeframe);
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
        private Balance GetBalance(string coin)
        {
            BalanceBase currentBalanceBase = BybitClient.GetBalanceBase(coin);
            BalanceData currentBalanceData = currentBalanceBase.Result.USDT;

            return new Balance(currentBalanceData.Wallet_balance, currentBalanceData.Available_balance);
        }
        private ObservableCollection<Position> GetPositions(string symbol)
        {
            PositionBase positionBase = BybitClient.GetPositionBase(symbol);
            List<PositionData> positionData = positionBase.result;

            ObservableCollection<Position> positions = new ObservableCollection<Position>();
            foreach (var pos in positionData)
            {
                positions.Add(new Position(pos.symbol, pos.side == "Buy" ? "Long" : "Short", pos.size, pos.position_value, pos.entry_price, pos.liq_price, pos.position_margin, pos.realised_pnl));
            }

            return positions;
        }
        private ObservableCollection<Order> GetOrders(string symbol, string orderStatus = null)
        {
            OrderBase orderBase = BybitClient.GetOrderBase(symbol, orderStatus);
            OrderPage orderPage = ((JObject)orderBase.result).ToObject<OrderPage>();
            List<OrderData> orderDatas = orderPage.data;

            ObservableCollection<Order> orders = new ObservableCollection<Order>();
            if (orderDatas != null)
            {
                foreach (var order in orderDatas)
                {
                    orders.Add(new Order(order.order_id, order.symbol, order.side, order.order_type, order.price, order.qty, order.order_status, order.take_profit, order.stop_loss, order.created_time));
                }
            }
            CountOrder = orders.Count;

            return orders;
        }
        private static List<PositionClosePnl> GetPositionsClosePnl(string symbol, string exec_type = null)
        {
            PositionClosePnlBase positionClosePnlBase = BybitClient.GetPositionsClosePnlBase(symbol, exec_type);
            PositionClosePnlPage positionClosePnlPage = positionClosePnlBase.result;
            List<PositionClosePnlData> positionClosePnlDatas = positionClosePnlPage.data;

            List<PositionClosePnl> positionClosePnls = new List<PositionClosePnl>();
            if (positionClosePnlDatas != null)
            {
                foreach (var posClosePnl in positionClosePnlDatas)
                {
                    positionClosePnls.Add(new PositionClosePnl(posClosePnl.symbol, posClosePnl.side == "Buy" ? "Short" : "Long", posClosePnl.qty, posClosePnl.avg_entry_price, posClosePnl.avg_exit_price, posClosePnl.closed_pnl, posClosePnl.exec_type, new DateTime(posClosePnl.created_at * 10000000L + BybitClient.Duration)));
                }
            }

            return positionClosePnls;
        }
        #endregion

        #endregion

        #region WebSockets
        private void GetNewMessage(string message)
        {
            //Console.WriteLine(message);

            if (message.Contains($"\"topic\":\"candle.{SelectedTimeframe}.{CurrentSymbol.alias}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewCandle(message);
                });
                return;
            }
            if (message.Contains($"\"topic\":\"orderBookL2_25.{CurrentSymbol.alias}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewOrderBook(message);
                });
                return;
            }
            if (message.Contains($"\"topic\":\"trade.{CurrentSymbol.alias}\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewTrade(message);
                });
                return;
            }
            if (message.Contains($"\"topic\":\"wallet\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewCurrentBalance(message);
                });
                return;
            }
            if (message.Contains($"\"topic\":\"position\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewPosition(message);
                });
                return;
            }
            if (message.Contains($"\"topic\":\"order\""))
            {
                App.Current.Dispatcher.InvokeAsync(() =>
                {
                    NewOrder(message);
                });
                return;
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
                DateTime openTime = new DateTime(klineWS.Start * 10000000L + BybitClient.Duration);

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

            foreach(var lastTradeData in lastTrades) 
            {
                Trade lastTrade = new Trade(lastTradeData.Price, lastTradeData.Size, lastTradeData.Timestamp, lastTradeData.Tick_direction, lastTradeData.Side);
                LastTrade = lastTrade;
                Trades.Insert(0, lastTrade);

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
                string longOrShort = pos.Side == "Buy" ? "Long" : "Short";
                if (pos.Symbol == CurrentSymbol.alias)
                {
                    Positions[Positions.IndexOf(Positions.First(p => p.Side == longOrShort))] = new Position(pos.Symbol, longOrShort, pos.Size, pos.Position_value, pos.Entry_price, pos.Liq_price, pos.Position_margin, pos.Realised_pnl);
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
                if (order.symbol == CurrentSymbol.alias)
                {
                    if (order.order_status == "New")
                    {
                        Orders.Insert(0, new Order(order.order_id, order.symbol, order.side, order.order_type, order.price, order.qty, order.order_status, order.take_profit, order.stop_loss, order.create_time));
                    }

                    if (order.order_status == "Cancelled" || order.order_status == "Filled")
                    {
                        Orders?.Remove(Orders.FirstOrDefault(ord => ord.Order_id == order.order_id));

                        if(order.order_status == "Filled" && order.order_type == "Limit")
                        {
                            if (order.side == "Buy")
                                Notifier.ShowSuccess($"Your entire order has been failed\n\nBought {order.qty} {CurrentSymbol.base_currency} contracts at {order.price} price.");
                            else
                                Notifier.ShowSuccess($"Your entire order has been failed\n\nSold {order.qty} {CurrentSymbol.base_currency} contracts at {order.price} price.");

                        }
                    }
                }
            }
            CountOrder = Orders.Count;
        }
        #endregion

        #endregion  
    }
}
    