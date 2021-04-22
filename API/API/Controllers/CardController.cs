using API.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        CustomDBContext db;

        public CardController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("add/{token}/{number}/{month_exp}/{year_exp}/{cvv}")]
        public Dictionary<string, dynamic> Add(string token, string number, string month_exp, string year_exp, string cvv)
        {
            if (!Card.CardValid(number))
                return new Dictionary<string, dynamic>() { { "added", false}, { "card_id", -1}, { "number", number} };

            if(db.Users.Where(u => u.Token == token).Count() == 0)
                return new Dictionary<string, dynamic>() { { "added", false }, { "card_id", -1 }, { "number", number } };

            var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;

            bool doMakeDefault = db.Cards.Where(c => c.OwnerId == userId).Count() == 0;
            bool exists = db.Cards.Where(c => c.CardNumber == number && c.OwnerId == userId).Count() == 1;

            if (!exists)
            {
                db.Cards.Add(new Card()
                {
                    CardNumber = number,
                    OwnerId = userId,
                    ExpMonth = month_exp,
                    ExpYear = year_exp,
                    CVV = cvv,
                    IsDefault = doMakeDefault,
                    Name = ""
                });
                db.SaveChanges();

                var cardId = db.Cards.Where(c => c.CardNumber == number && c.OwnerId == userId).First().Id;

                return new Dictionary<string, dynamic>() { { "added", true }, { "card_id", cardId }, { "number", number } };
            }
            else
            {
                return new Dictionary<string, dynamic>() { { "added", false }, { "card_id", -1 }, { "number", number } };
            }
        }

        [HttpGet("remove/{token}/{card_id}")]
        public Dictionary<string, dynamic> Remove(string token, int card_id)
        {
            var owner_id = db.Users.Where(u => u.Token == token).First().Id;
            var card = db.Cards.Where(c => c.Id == card_id && c.OwnerId == owner_id).First();
            try
            {
                db.Cards.Remove(card);
                db.SaveChanges();
                return new Dictionary<string, dynamic>(){ { "removed", true} };
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>() { { "removed", false } };
            }
        }

        [HttpGet("cards/{token}")]
        public List<Dictionary<string, dynamic>> Cards(string token)
        {
            List<Dictionary<string, dynamic>> res = new List<Dictionary<string, dynamic>>();

            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var cards = db.Cards.Where(c => c.OwnerId == userId);
                foreach(var card in cards)
                {
                    var cardInfo = new Dictionary<string, dynamic>();
                    cardInfo.Add("name", card.Name);
                    cardInfo.Add("number", card.CardNumber);
                    cardInfo.Add("id", card.Id);
                    cardInfo.Add("default", card.IsDefault);

                    res.Add(cardInfo);
                }
            }
            catch(Exception e)
            {
                res = new List<Dictionary<string, dynamic>>();
                return res;
            }

            return res;
        }

        [HttpGet("rename/{token}/{card_id}/{new_name}")]
        public Dictionary<string, dynamic> Rename(string token, int card_id, string new_name)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;

                var card = db.Cards.Where(c => c.OwnerId == userId && c.Id == card_id).FirstOrDefault();
                card.Name = new_name;
                db.SaveChanges();

                return new Dictionary<string, dynamic>() {
                    { "renamed", true},
                    { "new_name", card.Name}
                };    
            }
            catch(Exception e)
            {
                return new Dictionary<string, dynamic>() { 
                    { "renamed", false }, 
                    { "new_name", "" } 
                };
            }
        }

        [HttpGet("set_default/{token}/{card_id}")]
        public Dictionary<string, dynamic> SetDefault(string token, int card_id)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var defaultCard = db.Cards.Where(c => c.OwnerId == userId && c.IsDefault).FirstOrDefault();
                defaultCard.IsDefault = false;

                var card = db.Cards.Where(c => c.OwnerId == userId && c.Id == card_id).FirstOrDefault();
                card.IsDefault = true;

                return new Dictionary<string, dynamic>()
                {
                    { "success", true }
                };
            }
            catch(Exception e)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false }
                };
            }
        }
    }
}
