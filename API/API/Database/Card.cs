using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
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
    }
}
