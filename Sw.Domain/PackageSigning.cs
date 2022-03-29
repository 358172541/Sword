using Core;
using Domain.Enums;

namespace Domain
{
    /// <summary>
    /// 包裹签收
    /// </summary>
    public class PackageSigning : BaseEntity
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
        /// 唛头
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PackageSigningStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
