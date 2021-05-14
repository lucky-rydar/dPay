using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database
{
    public class Donation
    {
        public int Id;
        public string DonationToken;
        public int OwnerId;
        public int ReceiverCardId;
        public string Title;
        public string Description;
    }
}
