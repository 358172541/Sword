using System;

namespace Sword.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
        public string AvailableDisplay { get; set; }
    }
}
