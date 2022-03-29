using Core;
using Domain.Enums;

namespace Domain
{
    /// <summary>
    /// 会员收货地址
    /// </summary>
    public class MemberDeliveryAddress : BaseEntity
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
        /// 类型
        /// </summary>
        public MemberDeliveryAddressType Type { get; set; }

        /// <summary>
        /// 唛头
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
