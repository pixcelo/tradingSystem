namespace Zaku
{
    public interface IStrategy
    {
        string GetStrategyName();
        Position JudgeEntryCondition(Candle[] candles, int startIndex);
        Position JudgeCloseCondition(Candle cande, Position position);
    }
}
