using EventManagementSystem.Domain.EmployeeEntity.Exceptions;
using EventManagementSystem.Domain.EventAggregate.Exceptions;
using EventManagementSystem.Domain.ServiceEntity.Exceptions;
using EventManagementSystem.Domain.Shared.Exceptions;
using System;

namespace EventManagementSystem.Application.Middleware
{
    public static class Middleware
    {
        public static void HandleException(Exception e)
        {
            if (e.GetType() == typeof(InvalidNameException))
            {
                Console.WriteLine("Error: The provided name is invalid. Please ensure the name is not empty.");
            }
            else if (e.GetType() == typeof(CurrencyMismatchException))
            {
                Console.WriteLine("Error: Cannot perform the operation because currencies do not match.");
            }
            else if (e.GetType() == typeof(InvalidDNIException))
            {
                Console.WriteLine("Error: The provided DNI is invalid. It must contain 7 or 8 digits.");
            }
            else if (e.GetType() == typeof(InvalidServiceQuantityException))
            {
                Console.WriteLine("Error: Service quantity must be greater than zero.");
            }
            else if (e.GetType() == typeof(InvalidEventTimeSpanException))
            {
                Console.WriteLine("Error: The provided event timespan is invalid.");
            }
            else if (e.GetType() == typeof(InvalidEventDateTimeException))
            {
                Console.WriteLine("Error: The provided event date is invalid. The date cannot be in the past.");
            }
            else if (e.GetType() == typeof(EmployeeAlreadyExistException))
            {
                Console.WriteLine("Error: An employee with given DNI already exist. The DNI can not be repeated among employees.");
            }
            else if (e.GetType() == typeof(EmployeeDoesNotExistException))
            {
                Console.WriteLine("Error: An employee with given ID does not exist.");
            }
            else if (e.GetType() == typeof(ManagerIsAssignedToEventException))
            {
                Console.WriteLine("Error: A manager can not be removed if it is assigned to an event.");
            }
            else
            {
                Console.WriteLine("An unexpected error occurred. Press any key to restart the application...");
                Console.ReadKey();

            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }   
    }
}
