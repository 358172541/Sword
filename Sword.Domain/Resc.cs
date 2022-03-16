using Sword.Core;
using Sword.Domain.Enums;
using System;

namespace Sword.Domain
{
    public class Resc : BaseEntity
    {
        public Guid RescId { get; set; }
        public RescType Type { get; set; }
        public string Identity { get; set; }
        public string Icon { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
        public Guid? ParentId { get; set; }
    }
}
