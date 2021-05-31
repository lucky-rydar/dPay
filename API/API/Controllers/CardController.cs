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
        int baseTokenLength = 8;

        public CardController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("cheat_money/token")]
        public Dictionary<string, dynamic> CheatMoney(string token)
        {
            try
            {
                var user = db.Users.Where(u => u.Token == token).FirstOrDefault();
                var id = user.Id;

                var defaultCard = db.Cards.Where(c => c.OwnerId == id && c.IsDefault == true).FirstOrDefault();

                defaultCard.Balance = 999999.9f;
                db.SaveChanges();

                return new Dictionary<string, dynamic>()
                {
                    {"succeed", true }
                };
            }
            catch(Exception e)
            {
                return new Dictionary<string, dynamic>()
                { 
                    {"succeed", false }
                };
            }
        }

        [HttpGet("add/{token}/{name}/{currency}")]
        public Dictionary<string, dynamic> Add(string token, string name, string currency)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;

                bool doMakeDefault = db.Cards.Where(c => c.OwnerId == userId).Count() == 0;

                string cardToken = "";
                do
                {
                    cardToken = TokenGenerator.Generate(baseTokenLength);
                } while (db.Cards.Where(d => d.CardToken == cardToken).Count() != 0);

                db.Cards.Add(new Card()
                {
                    CardToken = cardToken,
                    OwnerId = userId,
                    IsDefault = doMakeDefault,
                    Name = name,
                    Balance = 0,
                    Currency = currency
                });
                db.SaveChanges();

                var cardId = db.Cards.Where(c => c.CardToken == cardToken && c.OwnerId == userId).First().Id;

                return new Dictionary<string, dynamic>() { { "added", true }, { "card_id", cardId }, { "card_token", cardToken } };
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>() { { "added", false }, { "card_id", null }, { "card_token", null } };
            }
        }

        [HttpGet("remove/{token}/{card_token}")]
        public Dictionary<string, dynamic> Remove(string token, string card_token)
        {
            var owner_id = db.Users.Where(u => u.Token == token).First().Id;
            var card = db.Cards.Where(c => c.CardToken == card_token && c.OwnerId == owner_id).First();
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
                    cardInfo.Add("id", card.Id);
                    cardInfo.Add("name", card.Name);
                    cardInfo.Add("card_token", card.CardToken);
                    cardInfo.Add("balance", card.Balance);
                    cardInfo.Add("currency", card.Currency);
                    cardInfo.Add("is_default", card.IsDefault);

                    res.Add(cardInfo);
                }
            }
            catch(Exception)
            {
                res = new List<Dictionary<string, dynamic>>();
                return res;
            }

            return res;
        }

        [HttpGet("rename/{token}/{card_token}/{new_name}")]
        public Dictionary<string, dynamic> Rename(string token, string card_token, string new_name)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;

                var card = db.Cards.Where(c => c.OwnerId == userId && c.CardToken == card_token).FirstOrDefault();
                card.Name = new_name;
                db.SaveChanges();

                return new Dictionary<string, dynamic>() {
                    { "renamed", true},
                    { "new_name", card.Name}
                };    
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>() { 
                    { "renamed", false }, 
                    { "new_name", null } 
                };
            }
        }

        [HttpGet("set_default/{token}/{card_token}")]
        public Dictionary<string, dynamic> SetDefault(string token, string card_token)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var defaultCard = db.Cards.Where(c => c.OwnerId == userId && c.IsDefault).FirstOrDefault();
                defaultCard.IsDefault = false;

                var card = db.Cards.Where(c => c.OwnerId == userId && c.CardToken == card_token).FirstOrDefault();
                card.IsDefault = true;

                return new Dictionary<string, dynamic>()
                {
                    { "success", true }
                };
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false }
                };
            }
        }

        [HttpGet("get_card_data/{token}/{card_token}")]
        public Dictionary<string, dynamic> GetCardData(string token, string card_token)
        {
            try
            {
                var userId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var card = db.Cards.Where(c => c.OwnerId == userId && c.CardToken == card_token).FirstOrDefault();
                return new Dictionary<string, dynamic>()
                {
                    { "name", card.Name},
                    { "card_token", card.CardToken},
                    { "balance", card.Balance },
                    { "currency", card.Currency },
                    { "is_default", card.IsDefault }
                };
            }
            catch(Exception)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "name", null },
                    { "card_token", null },
                    { "balance", null },
                    { "currency", null },
                    { "is_default", null }
                };

            }
        }
    }
}
