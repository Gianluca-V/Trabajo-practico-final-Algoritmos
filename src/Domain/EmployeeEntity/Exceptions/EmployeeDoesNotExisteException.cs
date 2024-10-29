using System;

namespace EventManagementSystem.Domain.EmployeeEntity.Exceptions
{
    public class EmployeeDoesNotExistException : Exception
    {
        public EmployeeDoesNotExistException(string message) : base(message) { }
    }
}
