using Core;
using System;

namespace Domain
{
    public class Role : BaseEntity
    {
        public Guid RoleId { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
    }
}
