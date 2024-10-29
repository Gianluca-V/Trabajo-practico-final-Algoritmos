using EventManagementSystem.Domain.Shared.Exceptions;

namespace EventManagementSystem.Domain.Shared.ValueObjects
{
    public class Money
    {
        public double amount { get; }
        public string currency { get; }

        public static Money operator * (Money a, double b){
            return Money.Make(a.amount*b,a.currency);
        }

        public static Money operator + (Money a, double b)
        {
            return Money.Make(a.amount + b, a.currency);
        }

        public static Money operator + (Money a, Money b)
        {
            if(a.currency != b.currency)
            {
                throw new CurrencyMismatchException("Added currencies do not match");
            }
            return Money.Make(a.amount + b.amount, a.currency);
        }

        private Money(double amount, string currency = "ARS")
        {
            this.amount = amount;
            this.currency = currency;
        }

        public static Money Make(double amount, string currency = "ARS")
        {
            return new Money(amount, currency);
        }

        public override string ToString() => $"${amount} {currency}";
    }
}
