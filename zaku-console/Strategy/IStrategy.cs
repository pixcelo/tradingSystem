namespace Zaku
{
    public interface IStrategy
    {
        Condition JudgeEntry(Candle[] candles, int startIndex);
        Condition JudgeClose(Candle cande, Position position);
    }
}
