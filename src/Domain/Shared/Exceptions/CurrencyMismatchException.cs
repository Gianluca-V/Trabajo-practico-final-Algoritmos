using System;

namespace EventManagementSystem.Domain.Shared.Exceptions
{
    public class CurrencyMismatchException : Exception
    {
        public CurrencyMismatchException(string message) : base(message) { }
    }
}
