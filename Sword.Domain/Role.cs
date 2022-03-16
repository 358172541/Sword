using Sword.Core;
using System;

namespace Sword.Domain
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
    }
}
