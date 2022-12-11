namespace Zaku
{
    public interface IStrategy
    {
        string GetStrategyName();
        Position JudgeEntryCondition(Candle[] candles, int startIndex);
        bool JudgeCloseCondition(Candle cande, Position position);
    }
}
