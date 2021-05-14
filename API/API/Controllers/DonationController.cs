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

        [HttpGet("create_donation/{token}/{receiver_card_token}/{title}/{description}")]
        public Dictionary<string, dynamic> CreateDonation(string token, string receiver_card_token/*if equals "null", so use default*/, string title, string description)
        {
            try
            {
                var user = db.Users.Where(u => u.Token == token).FirstOrDefault();
                Card receiverCard = new Card();

                if (receiver_card_token == "null")
                {
                    receiverCard = db.Cards.Where(c => c.OwnerId == user.Id && c.IsDefault == true).FirstOrDefault();
                }
                else
                {
                    receiverCard = db.Cards.Where(c => c.OwnerId == user.Id && c.CardToken == receiver_card_token).FirstOrDefault();
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
            catch (Exception e)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "donation_token", "" },
                    { "err", e.InnerException.Message }
                };
            }
        }

        [HttpGet("donations/{token}")]
        public List<Dictionary<string, dynamic>> Donations(string token)
        {
            try
            {
                List<Dictionary<string, dynamic>> res = new List<Dictionary<string, dynamic>>();

                var user = db.Users.Where(u => u.Token == token).FirstOrDefault();
                var donations = db.Donations.Where(d => d.OwnerId == user.Id).ToList();
                foreach(var donation in donations)
                {
                    var cardReceiver = db.Cards.Where(c => c.Id == donation.ReceiverCardId).FirstOrDefault();
                    
                    var donationInfo = new Dictionary<string, dynamic>()
                    {
                        { "title", donation.Title },
                        { "description", donation.Description },
                        { "donation_token", donation.DonationToken },
                        { "card_receiver", cardReceiver.CardToken }
                    };

                    res.Add(donationInfo);
                }

                return res;
            }
            catch(Exception)
            {
                return new List<Dictionary<string, dynamic>>();
            }
        }

        [HttpGet("donation_by_token/{donation_token}")]
        public Dictionary<string, dynamic> DonationByToken(string donation_token)
        {
            try
            {
                var donation = db.Donations.Where(d => d.DonationToken == donation_token).FirstOrDefault();
                var card = db.Cards.Where(c => c.Id == donation.ReceiverCardId).FirstOrDefault();

                return new Dictionary<string, dynamic>()
                {
                    { "exists", false },
                    { "title",  donation.Title },
                    { "description", donation.Description },
                    { "card_receiver", card.CardToken }
                };
            }
            catch (Exception)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "exists", false },
                    { "title",  "" },
                    { "exists", "" },
                    { "card_receiver", "" }

                };
            }
        }
    }
}
