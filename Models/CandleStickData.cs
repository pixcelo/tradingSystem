using System.Text.Json.Serialization;

namespace Zaku
{
    public class CandleStickData
    {
        [JsonPropertyName("60")]
        public List<List<decimal>>? Candlesticks { get; set; }
    }
}
