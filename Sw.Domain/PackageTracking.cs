using Core;

namespace Domain
{
    /// <summary>
    /// 包裹跟踪
    /// </summary>
    public class PackageTracking : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属包裹
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
