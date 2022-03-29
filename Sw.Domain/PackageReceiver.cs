using Core;

namespace Domain
{
    /// <summary>
    /// 包裹收货方
    /// </summary>
    public class PackageReceiver : BaseEntity
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
        /// 收货方姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 收货方电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 收货方手机号码
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// 收货方邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 收货方详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 收货方公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
