using System;

namespace Sword.Core
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime TokenExpireTime { get; set; }
        public DateTime TokenRefreshExpireTime { get; set; }
    }
}
