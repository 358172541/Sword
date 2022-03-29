﻿using Core;

namespace Domain
{
    /// <summary>
    /// 派送路线
    /// </summary>
    public class DeliveryRoute : BaseEntity
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
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
