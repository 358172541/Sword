using Core;
using Domain.Enums;

namespace Domain
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Identity { get; set; }

        /// <summary>
        /// 展示
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
