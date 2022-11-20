namespace Zaku
{
    public interface IDataService
    {
        string Path { get; set; }

        Candle[] GetTick();
    }
}
