namespace Zaku
{
    public class MovingAverageStrategy : IStrategy
    {
        public readonly string StategyName = "MovingAverageStrategy";

        public string GetStrategyName() => this.StategyName;

        /// <summary>
        /// エントリー条件を判定
        /// </summary>
        /// <param name="candles"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public Position JudgeEntryCondition(Candle[] candles, int startIndex)
        {
            var position = new Position();

            if (startIndex == 0)
            {
                return position;
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
                position.EntryCondition = true;
                position.Side = OrderSide.Buy;
            }

            return position;
        }

        /// <summary>
        /// 決済条件を判定
        /// </summary>
        /// <param name="candle"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool JudgeCloseCondition(Candle candle, Position position)
        {
            // 目標価額に達したらクローズ
            return candle.Close >= (position.EntryPrice * 1.01M)
                   ? true
                   : false;
        }
    }
}
