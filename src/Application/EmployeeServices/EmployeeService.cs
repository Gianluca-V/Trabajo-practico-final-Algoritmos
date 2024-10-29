using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.EmployeeEntity;
using EventManagementSystem.Domain.EmployeeEntity.Exceptions;
using EventManagementSystem.Domain.HallEntity;
using EventManagementSystem.Domain.ManagerEntity;
using EventManagementSystem.Domain.Repositories;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementSystem.Application.EmployeeServices
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IEventRepository eventRepository;

        public EmployeeService(IEmployeeRepository employeeRepository, IEventRepository eventRepository)
        {
            this.employeeRepository = employeeRepository;
            this.eventRepository = eventRepository;
        }

        public Employee AddEmployee(EmployeeRequest request)
        {
            if (request.Role.ToLower() == "manager")
            {
                var manager = Manager.Make(
                    request.Name,
                    request.DNI,
                    request.Salary,
                    request.Bonus);

                employeeRepository.AddEmployee(manager);
                return manager;
            }
            var employee = Employee.Make(
                request.Name,
                request.DNI,
                request.Salary,
                request.Task,
                request.Role);

            employeeRepository.AddEmployee(employee);
            return employee;
        }

        public Employee AddEmployee(Employee employee)
        {
            employeeRepository.AddEmployee(employee);
            return employee;
        }

        public Employee GetEmployeeById(Guid id) => employeeRepository.GetEmployeeById(id);
        public List<Employee> GetEmployees() => employeeRepository.GetEmployees();
        public List<Employee> GetEmployeesByRol(string rol)
        {
            var list = new List<Employee>();
            foreach (var el in employeeRepository.GetEmployees()){
                if (el.Role.ToLower().Equals(rol.ToLower()))
                {
                    list.Add(el);
                }
            }
            return list;
        }

        public void UpdateEmployee(Guid id, EmployeeRequest request)
        {
            Employee employee = employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                throw new EmployeeDoesNotExistException("An employee with given ID does not exist");
            }
            employee.Name = Name.Make(request.Name);
            employee.Dni = DNI.Make(request.DNI);
            employee.Salary = Money.Make(request.Salary);
            employee.Task = request.Task;
            employee.Role = request.Role;
            if (employee.Role.ToLower() == "manager")
            {
                employee = employee.AssignAsManager(request.Bonus);
            }
            employeeRepository.UpdateEmployee(employee);
        }
        public void UpdateEmployee(Employee employee)
        {
            employeeRepository.UpdateEmployee(employee);
        }

        public void RemoveEmployee(Guid id)
        {
            var employee = GetEmployeeById(id);
            if (employee.Role.ToLower() == "manager" && eventRepository.GetEvents()
                                                   .Where(e => e.AssignedManager == employee).ToList().Count != 0)
            {
                throw new ManagerIsAssignedToEventException("Can not remove a manager that is assigned to an event");
            }
            employeeRepository.RemoveEmployee(employee);
        }
        public void RemoveEmployee(Employee employee)
        {
            if (employee.Role.ToLower() == "manager" && eventRepository.GetEvents()
                                                   .Where(e => e.AssignedManager == employee).ToList().Count != 0)
            {
                throw new ManagerIsAssignedToEventException("Can not remove a manager that is assigned to an event");
            }
            employeeRepository.RemoveEmployee(employee);
        }
    }
}
