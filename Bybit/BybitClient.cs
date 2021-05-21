using Bybit.Api;
using Bybit.Client;
using Bybit.Model.Balance;
using Bybit.Model.LinearKline;
using Bybit.Model.Order;
using Bybit.Model.Position2;
using Bybit.Model.PositionClosePnl;
using Bybit.Model.ServerTime2;
using Bybit.Model.Symbol;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Websocket.Client;

namespace Bybit
{
    public static class BybitClient
    {
        private static WebsocketClient _wsPublic;
        private static WebsocketClient _wsPrivate;
        public static long Duration { get; set; }
        public static event Action<string> NewMessage;

        private static string _api_key;
        private static string _secret_key;

        public static void SetAPI_Key(string api_key)
        {
            _api_key = api_key;
        }
        public static void SetSecret_Key(string secret_key)
        {
            _secret_key = secret_key;
        }

        #region Статический конструктор
        static BybitClient()
        {
            Duration = GetTimeDuration();
        }
        #endregion

        #region WebSockets
        public static void RunBybitWS()
        {
            long expires = DateTime.Now.Ticks + 1000;
            string signature = Authentication.CreateSignature(_secret_key, "GET/realtime" + expires);
            _wsPublic = new WebsocketClient(new Uri("wss://stream.bytick.com/realtime_public"));
            _wsPrivate = new WebsocketClient(new Uri($"wss://stream.bytick.com/realtime_private?api_key={_api_key}&expires={expires}&signature={signature}"));
            _wsPublic.MessageReceived.Subscribe(message =>
            {
                NewMessage(message.Text);
            });
            _wsPrivate.MessageReceived.Subscribe(message =>
            {
                NewMessage(message.Text);
            });
            _wsPublic.Start();
            _wsPrivate.Start();

            Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(30000);
                    SendPublicWS("{\"op\":\"ping\"}");
                    SendPrivateWS("{\"op\":\"ping\"}");
                }
            });
        }

        #region public
        public static void SendPublicWS(string message)
        {
            _wsPublic.Send(message);
        }
        public static void ReconnectPublicWS()
        {
            _wsPublic.Reconnect();
        }
        #endregion
        #region private
        public static void SendPrivateWS(string message)
        {
            _wsPrivate.Send(message);
        }
        public static void ReconnectPrivateWS()
        {
            _wsPrivate.Reconnect();
        }
        #endregion

        #endregion

        #region HTTP

        #region public

        #region LinearKline
        public static LinearKlineBase GetLinearKlineBase(string pair, string timeframe)
        {
            long GetIntervalSeconds(string timeframeL)
            {
                switch (timeframeL)
                {
                    case "1":
                        return 1 * 60;
                    case "3":
                        return 3 * 60;
                    case "5":
                        return 5 * 60;
                    case "15":
                        return 15 * 60;
                    case "30":
                        return 30 * 60;
                    case "60":
                        return 60 * 60;
                    case "120":
                        return 120 * 60;
                    case "240":
                        return 240 * 60;
                    case "360":
                        return 360 * 60;
                    case "D":
                        return 24 * 60 * 60;
                    case "W":
                        return 7 * 24 * 60 * 60;
                    case "M":
                        return 30 * 24 * 60 * 60;
                    default:
                        throw new Exception("Invalid spacing format");
                }

            }

            var apiInstance = new LinearKlineApi();
            long from = GetTimeServer(Time.Seconds) - 200 * GetIntervalSeconds(timeframe);
            JObject result = (JObject)apiInstance.LinearKlineGet(pair, timeframe, from);

            return result.ToObject<LinearKlineBase>();
        }
        #endregion

        #region Symbol
        public static SymbolBase GetSymbolBase()
        {
            var apiInstance = new SymbolApi();
            JObject result = (JObject)apiInstance.SymbolGet();
            SymbolBase symbolBase = result.ToObject<SymbolBase>();
            foreach (var symbol in symbolBase.result)
            {
                symbol.lot_size_filter.GetQtyScale();
                symbol.price_filter.GetRound();
            }

            return symbolBase;
        }
        #endregion

        #endregion
        #region private

        #region Balance
        public static BalanceBase GetBalanceBase(string coin)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}&coin={coin}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new WalletApi();
            JObject result = (JObject)apiInstance.WalletGetBalance(coin);

            return result.ToObject<BalanceBase>();
        }
        #endregion

        #region Position
        public static PositionBase GetPositionBase(string symbol)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}&symbol={symbol}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new LinearPositionsApi();
            JObject result = (JObject)apiInstance.LinearPositionsMyPosition(symbol);

            return result.ToObject<PositionBase>();
        }
        #endregion

        #region Orders
        public static OrderBase GetOrderBase(string symbol, string orderStatus = null)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            string tempOrderStatus = null;
            if (orderStatus != null)
                tempOrderStatus = "&order_status=" + orderStatus;

            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}{tempOrderStatus}&symbol={symbol}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new LinearOrderApi();
            JObject result = (JObject)apiInstance.LinearOrderGetOrders(symbol: symbol, orderStatus: orderStatus);

            return result.ToObject<OrderBase>();
        }
        #endregion

        #region Cancel Order
        public static OrderBase CancelOrder(string symbol, string orderId)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}&order_id={orderId}&symbol={symbol}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new LinearOrderApi();
            JObject result = (JObject)apiInstance.LinearOrderCancel(symbol: symbol, orderId: orderId);

            return result.ToObject<OrderBase>();
        }
        #endregion

        #region Create Order
        public static OrderBase CreateLimitOrder(string side, string symbol, double qty, double price, string time_in_force, bool reduce_only, bool close_on_trigger)
        {
            return CreateOrder(side: side, symbol: symbol, order_type: "Limit", qty: qty, price: price, time_in_force: time_in_force, reduce_only: reduce_only, close_on_trigger: close_on_trigger);
        }
        public static OrderBase CreateMarketOrder(string side, string symbol, double qty, string time_in_force, bool reduce_only, bool close_on_trigger)
        {
            return CreateOrder(side: side, symbol: symbol, order_type: "Market", qty: qty, time_in_force: time_in_force, reduce_only: reduce_only, close_on_trigger: close_on_trigger);
        }
        private static OrderBase CreateOrder(string side, string symbol, string order_type, double qty, string time_in_force, bool reduce_only, bool close_on_trigger, double? price = null)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            string tempPrice = null;
            if (price != null)
                tempPrice = "&price=" + price;

            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}&close_on_trigger={close_on_trigger}&order_type={order_type}{tempPrice}&qty={qty}&reduce_only={reduce_only}&side={side}&symbol={symbol}&time_in_force={time_in_force}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new LinearOrderApi();
            JObject result = (JObject)apiInstance.LinearOrderNew(side: side, symbol: symbol, orderType: order_type, qty: qty, price: price, timeInForce: time_in_force, reduceOnly: reduce_only, closeOnTrigger: close_on_trigger);

            return result.ToObject<OrderBase>();
        }
        #endregion

        #region Positions Close Pnl
        public static PositionClosePnlBase GetPositionsClosePnlBase(string symbol, string exec_type = null)
        {
            long timestamp = GetTimeServer(Time.MiliSeconds);
            string tempExec_type = null;
            if (exec_type != null)
                tempExec_type = "&exec_type=" + exec_type;

            Configuration.Default.AddApiKey("api_key", _api_key);
            Configuration.Default.AddApiKey("sign", Authentication.CreateSignature(_secret_key, $"api_key={_api_key}{tempExec_type}&symbol={symbol}&timestamp={timestamp}"));
            Configuration.Default.AddApiKey("timestamp", timestamp.ToString());

            var apiInstance = new LinearPositionsApi();
            JObject result = (JObject)apiInstance.LinearPositionsClosePnlRecords(symbol: symbol, execType: exec_type);

            return result.ToObject<PositionClosePnlBase>();
        }
        #endregion

        #endregion

        #region Server
        public enum Time
        {
            Seconds,
            MiliSeconds,
            Tick
        }
        public static long GetTimeServer(Time time)
        {
            var apiInstance = new CommonApi();
            JObject result = (JObject)apiInstance.CommonGetTime();
            long timeServerSeconds = (long)result.ToObject<ServerTimeData>().Time_now;

            switch (time)
            {
                case Time.Seconds:
                    return timeServerSeconds;
                case Time.MiliSeconds:
                    return timeServerSeconds * 1000;
                case Time.Tick:
                    return timeServerSeconds * 10000000;
                default:
                    throw new Exception("Invalid time format");
            }
        }

        public static long GetTimeDuration()
        {
            var timeServerTick = GetTimeServer(Time.Tick);
            var timeNow = DateTime.Now.Ticks;

            return timeNow - timeServerTick;
        }
        public static void SetCultureUS()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        }
        #endregion

        #endregion
    }
}
