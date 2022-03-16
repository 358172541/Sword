using System;

namespace Sword.Core
{
    public class TokenRefreshRequest
    {
        public string Token { get; set; }
        public DateTime TokenExpireTime { get; set; }
        public DateTime TokenRefreshExpireTime { get; set; }
    }
}
