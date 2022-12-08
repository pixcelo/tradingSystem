namespace Zaku
{
    public interface IStrategy
    {
        string GetStrategyName();
        Condition JudgeEntry(Candle[] candles, int startIndex);
        Condition JudgeClose(Candle cande, Position position);
    }
}
