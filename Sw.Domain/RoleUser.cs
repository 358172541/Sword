namespace Domain
{
    /// <summary>
    /// 角色用户
    /// </summary>
    public class RoleUser
    {
        /// <summary>
        /// 所属角色
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 所属用户
        /// </summary>
        public int UserId { get; set; }
    }
}
