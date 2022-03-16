using System;

namespace Sword.Api
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message) { }
    }
}
