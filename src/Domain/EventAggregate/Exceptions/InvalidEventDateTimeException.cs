using System;

namespace EventManagementSystem.Domain.EventAggregate.Exceptions
{
    public class InvalidEventDateTimeException : Exception
    {
        public InvalidEventDateTimeException(string message) : base(message) { }
    }
}
