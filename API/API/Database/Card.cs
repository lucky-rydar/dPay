using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database
{
    public class Card
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public bool IsDefault { get; set; }
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }

        public static bool CardValid(string creditCardNumber)
        {
            if (string.IsNullOrEmpty(creditCardNumber))
                return false;

            int sumOfDigits = creditCardNumber.Where((e) => e >= '0' && e <= '9')
                            .Reverse()
                            .Select((e, i) => ((int)e - 48) * (i % 2 == 0 ? 1 : 2))
                            .Sum((e) => e / 10 + e % 10);

            return sumOfDigits % 10 == 0;
        }
    }
}
