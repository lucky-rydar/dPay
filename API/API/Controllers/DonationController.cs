using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Database;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        CustomDBContext db;
        int baseTokenLength = 8;

        public DonationController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("create_donation/{token}/{receiver_card_id}/{title}/{description}")]
        public Dictionary<string, dynamic> CreateDonation(string token, int receiver_card_id/*if (-1), so use default*/, string title, string description)
        {
            try
            {
                var user = db.Users.Where(u => u.Token == token).FirstOrDefault();
                Card receiverCard = new Card();

                if (receiver_card_id == -1)
                {
                    receiverCard = db.Cards.Where(c => c.OwnerId == user.Id && c.IsDefault == true).FirstOrDefault();
                }
                else
                {
                    receiverCard = db.Cards.Where(c => c.OwnerId == user.Id && c.Id == receiver_card_id).FirstOrDefault();
                }

                string donationToken = "";
                do
                {
                    donationToken = TokenGenerator.Generate(baseTokenLength);
                } while (db.Donations.Where(d => d.DonationToken == donationToken).Count() != 0);

                Donation d = new Donation()
                {
                    DonationToken = donationToken,
                    OwnerId = user.Id,
                    ReceiverCardId = receiverCard.Id,
                    Title = title,
                    Description = description
                };

                db.Donations.Add(d);
                db.SaveChanges();

                return new Dictionary<string, dynamic>()
                {
                    { "success", true },
                    { "donation_token", donationToken }
                };

            }
            catch (Exception)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "donation_token", "" }
                };
            }
        }
    }
}
