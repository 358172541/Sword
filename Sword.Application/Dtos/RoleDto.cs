using System;

namespace Application.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
        public string AvailableDisplay { get; set; }
    }
}
