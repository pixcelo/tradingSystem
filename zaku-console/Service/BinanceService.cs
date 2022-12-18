using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;

// https://jkorf.github.io/Binance.Net/
namespace Zaku
{
    /// <summary>
    /// Binance REST API
    /// </summary>
    public class BinanceService : IDataService
    {
        public string? Path { get; set; }
        private string Symbol { get; set; } = "BTCUSDT";
        private readonly BinanceClient exchange;

        public BinanceService()
        {
            string apiKey = Settings.GetApiKey();
            string apiSecret = Settings.GetApiSecret();

            this.exchange = new BinanceClient(new BinanceClientOptions
            {
                LogLevel = LogLevel.Debug,
                ApiCredentials = new ApiCredentials(apiKey, apiSecret)
            });
        }

        public async Task<Candle[]> GetTick()
        {
            var tradeHistory = await this.GetTradeHistoryData();
            var candles = new List<Candle>();

            foreach (var item in tradeHistory)
            {
                DateTimeOffset utcTime = new DateTimeOffset(item.TradeTime, TimeSpan.Zero);
                long unixTime = utcTime.ToUnixTimeSeconds();

                var c = new Candle();
                c.Date = unixTime;
                c.Close = item.Price;
                candles.Add(c);
            }

            return candles.ToArray();
        }

        // https://jkorf.github.io/Binance.Net/Examples.html#get-market-data-1
        public async Task<IEnumerable<Binance.Net.Interfaces.IBinanceRecentTrade>> GetTradeHistoryData()
        {
            var tradeHistoryData = await this.exchange.UsdFuturesApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
            if(!tradeHistoryData.Success)
            {
                Console.WriteLine("Request failed: " + tradeHistoryData.Error);
                return new List<Binance.Net.Interfaces.IBinanceRecentTrade>();
            }

            return tradeHistoryData.Data;

            // foreach (var item in spotTradeHistory)
            // {
            //     Console.WriteLine(item.OrderId);      // 2336973335
            //     Console.WriteLine(item.BaseQuantity); // 0.00070000
            //     Console.WriteLine(item.BuyerIsMaker); // True
            //     Console.WriteLine(item.IsBestMatch);  // True
            //     Console.WriteLine(item.Price);        // 17431.47000000
            //     Console.WriteLine(item.TradeTime);    // 2022/12/15 19:36:46
            // }
        }

        /// <summary>
        /// 売買注文を出す
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<bool> PlaceOrder(Position order)
        {
            var side = order.Side == OrderSide.Buy
                        ? (Binance.Net.Enums.OrderSide)OrderSide.Buy
                        : (Binance.Net.Enums.OrderSide)OrderSide.Sell;

            const int NUMBER_OF_REQUEST_TRIAL = 3;
            int counter = 1;

            while(NUMBER_OF_REQUEST_TRIAL > counter)
            {
                var openPositionResult = await exchange.UsdFuturesApi.Trading.PlaceOrderAsync(
                                            order.Symbol,
                                            side,
                                            FuturesOrderType.Market,
                                            order.EntryPrice);

                if(!openPositionResult.Success)
                {
                    Console.WriteLine("Request failed: " + openPositionResult.Error);
                    Console.WriteLine(Environment.NewLine + "try again Placing-Order.");
                    counter++;
                    continue;
                }

                return true;
            }

            Console.WriteLine("Request failed.");
            return false;
        }

        /// <summary>
        /// 指定したIDの注文をキャンセル
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<bool> CancelOrder(string orderId)
        {
            long id = Convert.ToInt64(orderId);
            var orderData = await exchange.UsdFuturesApi.Trading.CancelOrderAsync("BTCUSDT", id);
            if(!orderData.Success)
            {
                Console.WriteLine("Request failed: " + orderData.Error);
                return true;
            }

            Console.WriteLine($"Request success:  + order-id {orderId} canceled.");
            return false;
        }

        /// <summary>
        /// 注文履歴のリクエスト
        /// </summary>
        /// <returns></returns>
        public async Task<List<Position>> GetPositions()
        {
            var ordersData = await exchange.UsdFuturesApi.Trading.GetOrdersAsync(Symbol);
            if(!ordersData.Success)
            {
                Console.WriteLine("Request failed: " + ordersData.Error);
                return new List<Position>();
            }

            var data = ordersData.Data;

            return new List<Position>();
        }
    }
}
