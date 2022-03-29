using Core;

namespace Domain
{
    /// <summary>
    /// 会员派送公司
    /// </summary>
    public class MemberExpressCompany : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属会员
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// 徽标
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
