namespace Domain.Enums
{
    /// <summary>
    /// 货柜状态
    /// </summary>
    public enum ContainerStatus
    {
        /// <summary>
        /// 计划装柜
        /// </summary>
        Preparing = 1,

        /// <summary>
        /// 装柜中
        /// </summary>
        Loading = 2,

        /// <summary>
        /// 装柜完成
        /// </summary>
        Loaded = 3
    }
}
