using EventManagementSystem.Domain.Shared.BaseClasses;
using EventManagementSystem.Domain.Shared.ValueObjects;
using System.Text;

namespace EventManagementSystem.Domain.ClientEntity
{
    public class Client : BaseEntity
    {
        private Name name;
        private DNI dni;

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


        private Client(Name name, DNI dni)
        {
            this.name = name;
            this.dni = dni;
        }

        public static Client Make(string name, string dni)
        {
            return new Client(Name.Make(name), DNI.Make(dni));
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {this.Name.Value}");
            sb.AppendLine($"DNI: {this.Dni.Value}");
            return sb.ToString();
        }
    }
}
