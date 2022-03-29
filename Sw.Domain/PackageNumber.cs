using Core;

namespace Domain
{
    /// <summary>
    /// 包裹编号
    /// </summary>
    public class PackageNumber : BaseEntity
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
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
