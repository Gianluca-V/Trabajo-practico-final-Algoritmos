using System;

namespace EventManagementSystem.Domain.ClientEntity.Exceptions
{
    public class ClientDoesNotExistException : Exception
    {
        public ClientDoesNotExistException(string message) : base(message) { }
    }
}
