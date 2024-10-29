using System;

namespace EventManagementSystem.Domain.Shared.Exceptions
{
    public class InvalidDNIException : Exception
    {
        public InvalidDNIException(string message) : base(message) { }
    }
}
