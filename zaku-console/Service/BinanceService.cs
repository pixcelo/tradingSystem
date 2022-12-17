using Binance.Net.Clients;
using Binance.Net.Objects;
using CryptoExchange.Net.Authentication;

// https://jkorf.github.io/Binance.Net/
namespace Zaku
{
    /// <summary>
    /// APIServiceから呼び出す
    /// </summary>
    public class BinanceService
    {
        private readonly BinanceClient exchange;

        public BinanceService()
        {
            string apiKey = Settings.GetApiKey();
            string apiSecret = Settings.GetApiSecret();

            exchange = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(apiKey, apiSecret),
            });
        }

        public async Task<IEnumerable<Binance.Net.Interfaces.IBinanceRecentTrade>> GetTick()
        {
            var spotTradeHistoryData = await exchange.SpotApi.ExchangeData.GetTradeHistoryAsync("BTCUSDT");
            if(!spotTradeHistoryData.Success)
            {
                Console.WriteLine("Request failed: " + spotTradeHistoryData.Error);
                return new List<Binance.Net.Interfaces.IBinanceRecentTrade>();
            }

            return spotTradeHistoryData.Data;

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
    }
}
