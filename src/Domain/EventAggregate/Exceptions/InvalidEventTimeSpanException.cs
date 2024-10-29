using System;

namespace EventManagementSystem.Domain.EventAggregate.Exceptions
{
    public class InvalidEventTimeSpanException : Exception
    {
        public InvalidEventTimeSpanException(string message) : base(message) { }
    }
}
