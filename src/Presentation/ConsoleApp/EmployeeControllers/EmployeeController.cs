using EventManagementSystem.Application.EmployeeServices;
using EventManagementSystem.Contracts;
using EventManagementSystem.Domain.EmployeeEntity;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Linq;

namespace EventManagementSystem.Presentation.ConsoleApp.EmployeeControllers
{
    public class EmployeeController
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Employee Management ===");
            Console.WriteLine("1. Show Employees");
            Console.WriteLine("2. Add Employee");
            Console.WriteLine("3. Update Employee");
            Console.WriteLine("4. Delete Employee");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nSelect an option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    ShowEmployees();
                    break;
                case "2":
                    AddEmployee();
                    break;
                case "3":
                    UpdateEmployee();
                    break;
                case "4":
                    DeleteEmployee();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }

        private void ShowEmployees()
        {
            Console.Clear();
            Console.WriteLine("=== Employees ===");

            var employees = employeeService.GetEmployees();
            if (!employees.Any())
            {
                Console.WriteLine("No employees found.");
            }
            else
            {
                foreach (var employee in employees)
                {
                    Console.WriteLine(employee.ToString());
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Add New Employee ===");
            string name;
            HelperFunctions.PropertyValidation(out name, value => Name.Validate(value),
                "Name: ", "Employee name cannot be empty. Please try again.");

            string dni;
            HelperFunctions.PropertyValidation(out dni, value => DNI.Validate(value),
                "DNI: ", "Invalid employee DNI format. Please try again.");

            double salary;
            HelperFunctions.PropertyValidation(out salary, value => value != default,
                "Salary: ", "Invalid employee salary. Please try again.");

            Console.Write("Task: ");
            string task = Console.ReadLine();

            string role;
            HelperFunctions.PropertyValidation(out role, value => value.ToLower() == "employee" || value.ToLower() == "manager",
                "Role (Employee/Manager):", "Invalid employee role. Please try again.");

            double bonus = 0;
            if (role.ToLower() == "manager")
            {
                HelperFunctions.PropertyValidation(out bonus, value => value != default,
                    "Bonus: ", "Invalid manager bonus. Please try again.");
            }

            var employeeRequest = new EmployeeRequest
            {
                Name = name,
                DNI = dni,
                Salary = salary,
                Task = task,
                Role = role.ToLower(),
                Bonus = bonus
            };

            employeeService.AddEmployee(employeeRequest);
            Console.WriteLine("Employee added successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void UpdateEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Update Employee ===");

            var employees = employeeService.GetEmployees();
            if (!employees.Any())
            {
                Console.WriteLine("No employees available.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Employee employee;
            HelperFunctions.SelectItemFromList(out employee, employees, "\nAvailable employees:", "Select employee to update:");

            string name;
            HelperFunctions.PropertyValidation(out name, value => Name.Validate(value),
                "Name: ", "Employee name cannot be empty. Please try again.");

            string dni;
            HelperFunctions.PropertyValidation(out dni, value => DNI.Validate(value),
                "DNI: ", "Invalid employee DNI format. Please try again.");

            double salary;
            HelperFunctions.PropertyValidation(out salary, value => value != default,
                "Salary: ", "Invalid employee salary. Please try again.");

            Console.Write("Task: ");
            string task = Console.ReadLine();

            string role;
            HelperFunctions.PropertyValidation(out role, value => value.ToLower() == "employee" || value.ToLower() == "manager",
                "Role (Employee/Manager):", "Invalid employee role. Please try again.");

            double bonus = 0;
            if (role.ToLower() == "manager")
            {
                HelperFunctions.PropertyValidation(out bonus, value => value != default,
                    "Bonus: ", "Invalid manager bonus. Please try again.");
            }

            var employeeRequest = new EmployeeRequest
            {
                Name = name,
                DNI = dni,
                Salary = salary,
                Task = task,
                Role = role.ToLower(),
                Bonus = bonus
            };

            employeeService.UpdateEmployee(employee.Id, employeeRequest);
            Console.WriteLine("Employee updated successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("=== Delete Employee ===");

            var employees = employeeService.GetEmployees();
            if (!employees.Any())
            {
                Console.WriteLine("No employees available.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                return;
            }

            Employee employee;
            HelperFunctions.SelectItemFromList(out employee, employees, "\nAvailable employees:", "Select employee to delete:");

            employeeService.RemoveEmployee(employee.Id);
            Console.WriteLine("Employee deleted successfully!");

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
