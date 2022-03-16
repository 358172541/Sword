using System;
using System.Collections.Generic;

namespace Sword.Application.Dtos
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
        public string Version { get; set; }
    }
}
