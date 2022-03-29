using Core;
using Domain.Enums;

namespace Domain
{
    /// <summary>
    /// 车次
    /// </summary>
    public class Shift : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属路线
        /// </summary>
        public int DeliveryRouteId { get; set; }

        /// <summary>
        /// 派送公司名
        /// </summary>
        public string ExpressCompanyName { get; set; }

        /// <summary>
        /// 司机名
        /// </summary>
        public string DriverName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhoneNumber { get; set; }

        /// <summary>
        /// 车牌
        /// </summary>
        public string LicensePlateNumber { get; set; }

        /// <summary>
        /// 是否已检测通过
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ShiftStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
