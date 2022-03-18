﻿using Core;
using Domain.Enums;
using System;

namespace Domain
{
    public class User : BaseEntity
    {
        public Guid UserId { get; set; }
        public UserType Type { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public string Display { get; set; }
        public bool Available { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpireTime { get; set; }
        public DateTime? TokenRefreshExpireTime { get; set; }
    }
}
