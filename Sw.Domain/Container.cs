using Core;
using Domain.Enums;
using System;

namespace Domain
{
    /// <summary>
    /// 货柜
    /// </summary>
    public class Container : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所属地区
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 装柜时间
        /// </summary>
        public DateTime LoadingTime { get; set; }

        /// <summary>
        /// 开船时间
        /// </summary>
        public DateTime SailingTime { get; set; }

        /// <summary>
        /// 到港时间
        /// </summary>
        public DateTime ArrivingTime { get; set; }

        /// <summary>
        /// 内部柜号
        /// </summary>
        public string InternalContainerNumber { get; set; }

        /// <summary>
        /// 封条号
        /// </summary>
        public string SealNumber { get; set; }

        /// <summary>
        /// 船运公司
        /// </summary>
        public string ShippingCompany { get; set; }

        /// <summary>
        /// 调柜部门
        /// </summary>
        public string TransferDeparment { get; set; }

        /// <summary>
        /// 清关部门
        /// </summary>
        public string ClearanceDeparment { get; set; }

        /// <summary>
        /// 是否已通过检测
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public ContainerStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
