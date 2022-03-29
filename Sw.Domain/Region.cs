using Core;

namespace Domain
{
    /// <summary>
    /// 地区
    /// </summary>
    public class Region : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 筛选
        /// </summary>
        public string Screen { get; set; }

        /// <summary>
        /// 运单号前缀
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// 派送公司名
        /// </summary>
        public string ExpressCompanyName { get; set; }

        /// <summary>
        /// 派送公司详细地址
        /// </summary>
        public string ExpressCompanyDetailAddress { get; set; }

        /// <summary>
        /// 派送公司电话号码
        /// </summary>
        public string ExpressCompanyPhoneNumber { get; set; }

        /// <summary>
        /// 派送公司徽标
        /// </summary>
        public string ExpressCompanyLogo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
