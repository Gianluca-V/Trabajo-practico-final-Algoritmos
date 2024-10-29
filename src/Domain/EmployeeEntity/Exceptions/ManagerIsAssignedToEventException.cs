using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagementSystem.Domain.EmployeeEntity.Exceptions
{
    public class ManagerIsAssignedToEventException : Exception
    {
        public ManagerIsAssignedToEventException(string message) : base(message) { }
    }
}
