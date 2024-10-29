using EventManagementSystem.Domain.EventAggregate.Exceptions;
using System;

namespace EventManagementSystem.Domain.EventAggregate.ValueObjects
{
    public class EventDateTime
    {
        public DateTime Value { get; set; }

        private EventDateTime()
        {
            Value = new DateTime();
        }

        private EventDateTime(DateTime value)
        {
            Value = value;
        }

        public static EventDateTime Make(string value)
        {
            if (!Validate(value))
                throw new InvalidEventDateTimeException("Invalid event date, the date can not be in the past.");
            
            var result = DateTime.Parse(value);

            return new EventDateTime(result);
        }
        public static EventDateTime Make()
        {
            return new EventDateTime();
        }

        public static EventDateTime Make(DateTime value)
        {
            return new EventDateTime(value);
        }

        static public bool Validate(string value) 
        {
            DateTime temp;
            bool result = DateTime.TryParse(value, out temp);

            if (temp < DateTime.Now)
                result = false;

            return result;
        }
    }
}
