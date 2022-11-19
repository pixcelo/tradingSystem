using System.Text.Json;

namespace Zaku
{
    public class APIService : IDataService
    {
        public string path { get; set; }

        public void GetTick()
        {
            Console.WriteLine("This is the API Service");
        }
    }
}
