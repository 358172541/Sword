namespace Domain.Enums
{
    /// <summary>
    /// 包装类型
    /// </summary>
    public enum PackingType
    {
        /// <summary>
        /// 纸箱
        /// </summary>
        Carton = 1,

        /// <summary>
        /// 编织袋
        /// </summary>
        WovenBag = 2,

        /// <summary>
        /// 木架
        /// </summary>
        WoodenFrame = 3,

        /// <summary>
        /// 裸装
        /// </summary>
        Naked = 4,

        /// <summary>
        /// 重货
        /// </summary>
        HeavyCargo,

        /// <summary>
        /// 超长
        /// </summary>
        OverLong,

        /// <summary>
        /// 塑料桶
        /// </summary>
        PlasticBucket,

        /// <summary>
        /// 托盘
        /// </summary>
        Tray
    }
}
