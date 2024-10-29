using EventManagementSystem.Domain.ManagerEntity;
using EventManagementSystem.Domain.Shared.BaseClasses;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Text;

namespace EventManagementSystem.Domain.EmployeeEntity
{
    public class Employee : BaseEntity
    {
        protected Name name;
        protected DNI dni;
        protected Money salary;
        protected string task;
        protected string role;

        public Name Name
        {
            get => name;
            set => name = value;
        }

        public DNI Dni
        {
            get => dni;
            set => dni = value;
        }

        public Money Salary
        {
            get => salary;
            set => salary = value;
        }
        public string Task
        {
            get => task;
            set => task = value;
        }

        public string Role
        {
            get => role;
            set => role = value;
        }

        protected Employee(Name name, DNI dni, Money salary, string task, string role)
        {
            this.name = name;
            this.dni = dni;
            this.salary = salary;
            this.role = role;
            this.task = task;
        }
        protected Employee(Guid id,Name name, DNI dni, Money salary, string task, string role) : base(id)
        {
            this.name = name;
            this.dni = dni;
            this.salary = salary;
            this.task = task;
            this.role = role;
        }

        public static Employee Make(string name, string dni, double salary, string task, string role)
        {
            return new Employee(
                Name.Make(name),
                DNI.Make(dni),
                Money.Make(salary),
                task,
                role);
        }

        public Manager AssignAsManager(double bonus)
        {
            return Manager.Make(this,bonus);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ID: {this.Id}");
            sb.AppendLine($"Name: {this.Name.Value}");
            sb.AppendLine($"DNI: {this.Dni.Value}");
            sb.AppendLine($"Task: {this.Task}");
            sb.AppendLine($"Role: {this.role}");
            sb.AppendLine($"Salary: {this.salary}");
            return sb.ToString();
        }
    }
}
