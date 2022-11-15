using System.Text.Json;

namespace Zaku
{
    public class JsonService
    {
        private readonly string _filePath;

        public JsonService(string filePath)
        {
            this._filePath = filePath;
        }

        /// <summary>
        /// Deserialize Json to .NET Object
        /// </summary>
        /// <returns></returns>
        public CandleStickData? SerializeBasicModel()
        {
            return JsonSerializer.Deserialize<CandleStickData>(this._filePath);
        }

    }
}
