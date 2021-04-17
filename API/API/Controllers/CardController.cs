﻿using API.Database;
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
        AddCardStatus Add(string token, string number, string month_exp, string year_exp, string cvv)
        {
            if (!Card.CardValid(number))
                return new AddCardStatus() { Added = false, CardId = -1, Number = number };

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

                return new AddCardStatus() { Added = true, CardId = cardId, Number = number };
            }
            else
            {
                return new AddCardStatus() { Added = false, CardId = -1, Number = number };
            }
        }

        [HttpGet("remove/{token}/{card_id}")]
        bool Remove(string token, int card_id)
        {
            var owner_id = db.Users.Where(u => u.Token == token).First().Id;
            var card = db.Cards.Where(c => c.Id == card_id && c.OwnerId == owner_id).First();
            try
            {
                db.Cards.Remove(card);
                db.SaveChanges();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
