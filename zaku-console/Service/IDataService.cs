namespace Zaku
{
    public interface IDataService
    {
        string Path { get; set; }

        Task<Candle[]> GetTick();
    }
}
