using EventManagementSystem.Domain.EventAggregate.ValueObjects;
using System;
using System.Collections.Generic;

namespace EventManagementSystem.Presentation.ConsoleApp
{
    static public class HelperFunctions
    {
        public static void PropertyValidation<T>(out T property, Predicate<T> predicate, string inputMessage, string errorMessage)
        {
            do
            {
                Console.Write(inputMessage);
                string value = Console.ReadLine();
                try
                {
                    var convertedValue = (T)Convert.ChangeType(value, typeof(T));

                    if (predicate(convertedValue))
                    {
                        property = convertedValue;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }

            } while (true);
        }

        public static void PropertyValidation(out EventDateTime property, string inputMessage, string errorMessage)
        {
            do
            {
                Console.Write(inputMessage);
                string value = Console.ReadLine();
                try
                {

                    if (EventDateTime.Validate(value))
                    {
                        var convertedValue = EventDateTime.Make(value);
                        property = convertedValue;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }

            } while (true);
        }

        public static void PropertyValidation(out EventTimeSpan property, string inputMessage, string errorMessage)
        {
            do
            {
                Console.Write(inputMessage);
                Console.WriteLine("\nStart time:");
                string startTime = Console.ReadLine();

                if (!EventTimeSpan.SingleValueValidate(startTime))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }

                Console.WriteLine("End time:");
                string endTime = Console.ReadLine();
                if (!EventTimeSpan.SingleValueValidate(startTime))
                {
                    Console.WriteLine(errorMessage);
                    continue;
                }
                try
                {

                    if (EventTimeSpan.Validate(startTime,endTime))
                    {
                        var convertedValue = EventTimeSpan.Make(startTime,endTime);
                        property = convertedValue;
                        break;
                    }
                    else
                    {
                        Console.WriteLine(errorMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }

            } while (true);
        }

        public static void SelectItemFromList<T>(out T property, IList<T> list, string listMessage, string inputMessage)
        {
            int index;
            int lenght = list.Count;
            do
            {
                Console.WriteLine(listMessage);
                for (int i = 0; i < lenght; i++)
                {
                    Console.WriteLine($"{i}: \n {list[i]}");
                    Console.WriteLine();
                }

                Console.Write(inputMessage);
                if (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= lenght)
                {
                    Console.WriteLine("Invalid value selected. Please try again.");
                }
                else
                {
                    property = list[index];
                    break;
                }
            } while (true);

            
        }
    }
}
