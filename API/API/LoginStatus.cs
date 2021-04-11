using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class LoginStatus
    {
        public bool Logined { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
    }
}
