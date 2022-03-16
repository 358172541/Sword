using System;

namespace Sword.Api
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message) { }
    }
}
