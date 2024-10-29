using EventManagementSystem.Domain.EmployeeEntity;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void RemoveEmployee(Employee employee);
        Employee GetEmployeeById(Guid id);
        Employee GetEmployeeByDNI(string dni);
        void UpdateEmployee(Employee employee);
        List<Employee> GetEmployees();
    }
}
