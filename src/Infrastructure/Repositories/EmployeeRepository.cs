using EventManagementSystem.Domain.EmployeeEntity;
using EventManagementSystem.Domain.EmployeeEntity.Exceptions;
using EventManagementSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementSystem.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> employees = new List<Employee>();

        public void AddEmployee(Employee employee)
        {
            if(GetEmployeeByDNI(employee.Dni.Value) != null)
            {
                throw new EmployeeAlreadyExistException("An employee with given DNI already exist");
            }
            employees.Add(employee);
        }
        public void RemoveEmployee(Employee employee)
        {
            Employee temp = GetEmployeeById(employee.Id);
            if (temp == null)
            {
                throw new EmployeeDoesNotExistException("An employee with given ID does not exist");
            }
            employees.Remove(temp);
        }
        public Employee GetEmployeeById(Guid id) => employees.FirstOrDefault(e => e.Id == id);
        public Employee GetEmployeeByDNI(string dni)
        {
            return employees.Where(e => e.Dni.Value == dni).FirstOrDefault();
        }

        public void UpdateEmployee(Employee employee)
        {
            
            if(GetEmployeeById(employee.Id) == null)
            {
                throw new EmployeeDoesNotExistException("An employee with given ID does not exist");
            }

            RemoveEmployee(employee);
            AddEmployee(employee);
        }
        public List<Employee> GetEmployees() => employees.ToList();
    }
}
