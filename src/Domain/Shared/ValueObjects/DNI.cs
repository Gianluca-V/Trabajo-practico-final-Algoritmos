using EventManagementSystem.Domain.Shared.Exceptions;

namespace EventManagementSystem.Domain.Shared.ValueObjects
{
    public class DNI
    {
        private readonly string value;

        public string Value => value;
        private DNI(string value)
        {
            this.value = value;
        }

        public static DNI Make(string value)
        {
            if (!Validate(value))
                throw new InvalidDNIException("Invalid DNI format");

            return new DNI(value);
        }

        public static bool Validate(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length == 7 || value.Length == 8;
        }
    }
}
