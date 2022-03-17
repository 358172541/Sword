using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public abstract class BaseEntity
    {
        public DateTime CreateTime { get; set; }
        public Guid Creator { get; set; }
        public DateTime UpdateTime { get; set; }
        public Guid Updator { get; set; }
        public DateTime DeleteTime { get; set; }
        public Guid Deletor { get; set; }
        [Timestamp] public byte[] Version { get; set; }
    }
}
