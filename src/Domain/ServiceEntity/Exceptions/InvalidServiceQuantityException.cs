using System;

namespace EventManagementSystem.Domain.ServiceEntity.Exceptions
{
    public class InvalidServiceQuantityException : Exception
    {
        public InvalidServiceQuantityException(string message) : base(message) { }
    }
}
