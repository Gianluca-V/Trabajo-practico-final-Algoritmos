using EventManagementSystem.Domain.Shared.ValueObjects;
using System;
using System.Text;
using EventManagementSystem.Domain.EmployeeEntity;

namespace EventManagementSystem.Domain.ManagerEntity
{
    public class Manager : Employee
    {
        private Money bonus;
        public Money Bonus => bonus;

        private Manager(Name name, DNI dni, Money salary, string role, Money bonus)
            : base(name, dni, salary, "Manage the event", role)
        {
            this.bonus = bonus;
        }
        private Manager(Guid id,Name name, DNI dni, Money salary, string role, Money bonus)
            : base(id,name, dni,salary, "Manage the event", role)
        {
            this.bonus = bonus;
        }

        public static Manager Make(string name, string dni, double salary, double bonus)
        {
            return new Manager(
                Name.Make(name),
                DNI.Make(dni),
                Money.Make(salary),
                "Manager",
                Money.Make(bonus));
        }
        public static Manager Make(Employee employee, double bonus)
        {
            return new Manager(
                employee.Id,
                employee.Name,
                employee.Dni,
                employee.Salary,
                "Manager",
                Money.Make(bonus));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"ID: {this.Id}");
            sb.AppendLine($"Name: {this.Name.Value}");
            sb.AppendLine($"DNI: {this.Dni.Value}");
            sb.AppendLine($"Task: {this.task}");
            sb.AppendLine($"Role: {this.role}");
            sb.AppendLine($"Salary: {this.salary}");
            sb.AppendLine($"Bonus: {this.bonus}");
            return sb.ToString();
        }
    }
}
