using EventManagementSystem.Domain.EventAggregate.Exceptions;
using System;

namespace EventManagementSystem.Domain.EventAggregate.ValueObjects
{
    public class EventTimeSpan
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        private EventTimeSpan()
        {
            StartTime = new TimeSpan();
            EndTime = new TimeSpan();
        }

        private EventTimeSpan(TimeSpan start, TimeSpan end)
        {
            StartTime = start;
            EndTime = end;
        }

        public static EventTimeSpan Make(string start, string end)
        {
            TimeSpan startTime = new TimeSpan();
            TimeSpan.TryParseExact(start, @"hh\:mm", null, out startTime);

            TimeSpan endTime = new TimeSpan();
            TimeSpan.TryParseExact(end, @"hh\:mm", null, out endTime);

            if (!Validate(start,end))
                throw new InvalidEventTimeSpanException("Invalid event time");

            return new EventTimeSpan(startTime, endTime);
        }
        public static EventTimeSpan Make()
        {
            return new EventTimeSpan();
        }

        public static EventTimeSpan Make(TimeSpan start, TimeSpan end)
        {
            return new EventTimeSpan(start,end);
        }

        static public bool Validate(string value, string end)
        {
            TimeSpan startTemp;
            bool isStartValid = TimeSpan.TryParseExact(value, @"hh\:mm", null, out startTemp);
            TimeSpan endTemp;
            bool isEndValid = TimeSpan.TryParseExact(end, @"hh\:mm", null, out endTemp);

            bool isValid = isStartValid && isEndValid && startTemp < endTemp;
            return isValid;
        }

        static public bool SingleValueValidate(string value)
        {
            TimeSpan temp;
            bool isValid = TimeSpan.TryParseExact(value, @"hh\:mm", null, out temp);
            return isValid;
        }
    }
}
