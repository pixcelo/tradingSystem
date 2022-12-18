namespace Zaku;

public class ExchangeService : IDataService
{
    public string? Path { get; set; }
    private readonly HttpClient client;

    public ExchangeService(HttpClient client)
    {
        this.client = client;
        //this.client.BaseAddress = new Uri("https://api1.binance.com");
    }

    // https://binance-docs.github.io/apidocs/spot/en/#kline-candlestick-data
    public async Task<Candle[]> GetTick()
    {
        var response = await client.GetAsync("https://api1.binance.com/api/v3/klines?symbol=BTCUSDT&interval=5m");

        return new List<Candle>().ToArray();
    }

    public async Task<List<Position>> GetPositions()
    {
        return new List<Position>();
    }

    public async Task<bool> PlaceOrder(Position order)
    {
        return true;
    }
}
