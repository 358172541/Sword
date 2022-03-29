using Core;
using Domain.Enums;
using System;

namespace Domain
{
    /// <summary>
    /// 包裹
    /// </summary>
    public class Package : BaseEntity
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
        /// 所属地区
        /// </summary>
        public int RegionId { get; set; }

        /// <summary>
        /// 所属派送地区
        /// </summary>
        public int DeliveryRegionId { get; set; }

        /// <summary>
        /// 所属柜号
        /// </summary>
        public int ContainerId { get; set; }

        /// <summary>
        /// 所属车次
        /// </summary>
        public int ShiftId { get; set; }

        /// <summary>
        /// 包装类型
        /// </summary>
        public PackingType PackingType { get; set; }

        /// <summary>
        /// 货物类型
        /// </summary>
        public ItemType ItemType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public PackageStatus Status { get; set; }

        /// <summary>
        /// 派送状态
        /// </summary>
        public DeliveryStatus DeliveryStatus { get; set; }

        /// <summary>
        /// 结算状态
        /// </summary>
        public BalanceStatus BalanceStatus { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string ExpressNumber { get; set; }

        /// <summary>
        /// 物品名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 物品数量（件）
        /// </summary>
        public int ItemCount { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// 唛头
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// 发货方
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 送货方电话号码
        /// </summary>
        public string DeliverPhoneNumber { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 快递列表
        /// </summary>
        public string ExpressList { get; set; }

        /// <summary>
        /// 具体尺寸
        /// </summary>
        public string DetailSize { get; set; }

        /// <summary>
        /// 超重货体积/材积重
        /// </summary>
        public decimal VolumeWeight { get; set; }

        /// <summary>
        /// 重量比例
        /// </summary>
        public decimal WeightPercentage { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// S/体积
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// T/体积
        /// </summary>
        public decimal ConfirmedVolume { get; set; }

        /// <summary>
        /// 客户结算体积
        /// </summary>
        public decimal CustomerConfirmedVolume { get; set; }

        /// <summary>
        /// 代收
        /// </summary>
        public decimal Collection { get; set; }

        /// <summary>
        /// 总运费
        /// </summary>
        public decimal TotalFreight { get; set; }

        /// <summary>
        /// 已收运费金额
        /// </summary>
        public decimal ReceivedFreight { get; set; }

        /// <summary>
        /// 杂费
        /// </summary>
        public decimal Misc { get; set; }

        /// <summary>
        /// 打包图片
        /// </summary>
        public string PackingPicture { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否已打印
        /// </summary>
        public bool IsPrinted { get; set; }

        /// <summary>
        /// 提货时间
        /// </summary>
        public DateTime PickupTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
