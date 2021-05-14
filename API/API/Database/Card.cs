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
        public string CardToken { get; set; }
        public float Balance { get; set; }
        public string Currency { get; set; }
    }
}
