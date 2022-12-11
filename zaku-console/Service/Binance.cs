using System.Net.Http.Headers;
using Binance.Net.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;

// https://jkorf.github.io/Binance.Net/
namespace Zaku
{
    /// <summary>
    /// APIServiceから呼び出す
    /// </summary>
    public class Binance
    {
        private readonly BinanceClient exchange;

        public Binance()
        {
            exchange = new BinanceClient(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
            });
        }
    }
}
