using System;

namespace EventManagementSystem.Domain.EmployeeEntity.Exceptions
{
    public class EmployeeAlreadyExistException : Exception
    {
        public EmployeeAlreadyExistException(string message) : base(message) { }
    }
}
