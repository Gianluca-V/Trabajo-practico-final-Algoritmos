using System;

namespace EventManagementSystem.Domain.Shared.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }
}
