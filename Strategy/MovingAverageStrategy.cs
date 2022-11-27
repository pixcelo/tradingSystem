namespace Zaku
{
    public class MovingAverageStrategy : IStrategy
    {
        public Condition JudgeEntry(Candle[] candles, int startIndex)
        {
            var condition = new Condition();

            if (startIndex == 0)
            {
                return condition;
            }

            int windowSizeShort = 20;
            int windowSizeLong = 50;
            int prevIndex = startIndex - 1;

            var prevShort = MovingAverage.GetSimpleMovingAverage(candles, windowSizeShort, prevIndex);
            var prevLong = MovingAverage.GetSimpleMovingAverage(candles, windowSizeLong, prevIndex);
            var maShort = MovingAverage.GetSimpleMovingAverage(candles, windowSizeShort, startIndex);
            var maLong = MovingAverage.GetSimpleMovingAverage(candles, windowSizeLong, startIndex);

            // 短期線が長期線を上抜け
            if (prevShort < prevLong && maShort > maLong)
            {
                condition.IsOk = true;
                condition.Side = OrderSide.Buy;
            }

            return condition;
        }

        public Condition JudgeClose(Position position)
        {
            return new Condition();
        }

    }
}