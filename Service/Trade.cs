namespace Zaku
{
    public class Trade
    {
        private readonly IDataService dataService;

        public Trade(IDataService dataService)
        {
            this.dataService = dataService;
        }

        public void SetPath(string path)
        {
            this.dataService.path = path;
        }

        public void GetTick()
        {
            this.dataService.GetTick();
        }
    }
}
