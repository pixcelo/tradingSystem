namespace Zaku
{
    public interface IStrategy
    {
        string GetStrategyName();
        Position JudgeEntryCondition(Candle[] candles, int startIndex);
        Position JudgeEntryCondition(Candle[] candles);
        bool JudgeCloseCondition(Candle cande, Position position);
    }
}
