namespace Zaku
{
    public static class Calucrator
    {
        /// <summary>
        /// 支払い手数料
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="commissionRate"></param>
        /// <returns></returns>
        public static decimal GetCommissionPaid(
            decimal amount,
            decimal commissionRate)
        {
            return amount * commissionRate;
        }
    }
}
