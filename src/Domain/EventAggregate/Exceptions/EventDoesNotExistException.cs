using System;

namespace EventManagementSystem.Domain.EventAggregate.Exceptions
{
    public class EventDoesNotExistException : Exception
    {
        public EventDoesNotExistException(string message) : base(message) { }
    }
}
