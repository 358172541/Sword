using System;

namespace Api.Exceptions
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message) { }
    }
}
