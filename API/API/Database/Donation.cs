using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Database
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonationToken { get; set; }
        public int OwnerId { get; set; }
        public int ReceiverCardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
