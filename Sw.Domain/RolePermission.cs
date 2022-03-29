namespace Domain
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RolePermission
    {
        /// <summary>
        /// 所属角色
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 所属权限
        /// </summary>
        public int PermissionId { get; set; }
    }
}
