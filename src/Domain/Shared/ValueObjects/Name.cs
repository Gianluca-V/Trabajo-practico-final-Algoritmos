using EventManagementSystem.Domain.Shared.Exceptions;


namespace EventManagementSystem.Domain.Shared.ValueObjects
{
    public class Name
    {
        private readonly string value;
        public string Value => value;

        private Name(string value)
        {
            this.value = value;
        }

        public static Name Make(string value)
        {
            if (!Validate(value))
                throw new InvalidNameException("Name cannot be empty");

            return new Name(value);
        }

        public static bool Validate(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
