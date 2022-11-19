namespace Zaku
{
    public interface IDataService
    {
        string path { get; set; }

        void GetTick();
    }
}
