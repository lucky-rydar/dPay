using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database
{
    public class Transaction
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string DateTime { get; set; }
        public string FromCard { get; set; }
        public string ToCard { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; }
    }
}
