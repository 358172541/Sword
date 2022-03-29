namespace Domain.Enums
{
    /// <summary>
    /// 结算状态
    /// </summary>
    public enum BalanceStatus
    {
        /// <summary>
        /// 未付款
        /// </summary>
        Unpaid = 1,

        /// <summary>
        /// 已导出账单
        /// </summary>
        BillExported = 2,

        /// <summary>
        /// 待结算
        /// </summary>
        BeforeSettled = 3
    }
}
