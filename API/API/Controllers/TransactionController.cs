using API.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        CustomDBContext db;

        public TransactionController(CustomDBContext db)
        {
            this.db = db;
            this.db.Database.EnsureCreated();
        }

        [HttpGet("send_by_card/{token}/{from_card}/{to_card}/{amount}")]
        public Dictionary<string, dynamic> SendByCard(string token, string from_card, string to_card, float amount)
        {
            try
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

                var userFrom = db.Users.Where(u => u.Token == token).FirstOrDefault();
                var fromCard = db.Cards.Where(c => c.CardToken == from_card).FirstOrDefault();
                var toCard = db.Cards.Where(c => c.CardToken == to_card).FirstOrDefault();
                if (amount <= 0)
                    throw new Exception();
                if (fromCard.OwnerId != userFrom.Id)
                    throw new Exception();
                if (fromCard.Currency != toCard.Currency)
                    throw new Exception();
                if (fromCard.Balance < amount)
                    throw new Exception();

                toCard.Balance += amount;
                fromCard.Balance -= amount;

                int orderId = 1;
                try
                {
                    orderId = db.Transactions.Max(t => t.Id) + 1;
                }
                catch(Exception) { }

                db.Transactions.Add(new Transaction()
                {
                    Id = orderId,
                    Success = true,
                    DateTime = DateTime.Now.ToString(),
                    FromCard = fromCard.CardToken,
                    ToCard = toCard.CardToken,
                    Amount = amount,
                    Currency = fromCard.Currency
                });
                
                db.SaveChanges();
                
                return new Dictionary<string, dynamic>()
                {
                    { "success", true },
                    { "order_id", orderId },
                    { "amount", amount }
                };
                
            }
            catch (Exception)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "order_id", null },
                    { "amount", null }
                };
            }
        }

        [HttpGet("send_by_username/{token}/{from_username}/{to_username}/{amount}")]
        public Dictionary<string, dynamic> SendByUsername(string token, string from_username, string to_username, float amount)
        {
            try
            {
                var fromUser = db.Users.Where(u => u.Username == from_username).FirstOrDefault();
                var toUser = db.Users.Where(u => u.Username == to_username).FirstOrDefault();
                var fromCard = db.Cards.Where(c => c.OwnerId == fromUser.Id && c.IsDefault).FirstOrDefault();
                var toCard = db.Cards.Where(c => c.OwnerId == toUser.Id && c.IsDefault).FirstOrDefault();

                if (amount <= 0)
                    throw new Exception("invalid amount");
                if (fromCard.Currency != toCard.Currency)
                    throw new Exception("not the same currency");
                if (fromCard.Balance < amount)
                    throw new Exception("not enough money");

                fromCard.Balance -= amount;
                toCard.Balance += amount;

                int orderId = 1;
                try
                {
                    orderId = db.Transactions.Max(t => t.Id) + 1;
                }
                catch (Exception) { }

                db.Transactions.Add(new Transaction()
                {
                    Id = orderId,
                    Success = true,
                    DateTime = DateTime.Now.ToString(),
                    FromCard = fromCard.CardToken,
                    ToCard = toCard.CardToken,
                    Amount = amount,
                    Currency = fromCard.Currency
                });

                db.SaveChanges();

                return new Dictionary<string, dynamic>()
                {
                    { "success", true },
                    { "amount", amount },
                    { "order_id", orderId }
                };
            }
            catch(Exception e)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "amount", null },
                    { "order_id", null },
                    { "err", e.Message}
                };
            }
        }

        [HttpGet("transactions/{token}")]
        public List<Dictionary<string, dynamic>> Transactions(string token)
        {
            try
            {
                var cardIds = new List<int>();
                var ownerId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var cards = db.Cards.Where(c => c.OwnerId == ownerId).ToList();
                List<Dictionary<string, dynamic>> transactions = new List<Dictionary<string, dynamic>>();
                
                foreach(var card in cards)
                {
                    var cardToken = card.CardToken;
                    if(db.Transactions.Where(t=>t.FromCard == cardToken).Count() > 0)
                    {
                        var sentTransactions = db.Transactions.Where(t => t.FromCard == cardToken).ToList();
                        foreach(var t in sentTransactions)
                        {
                            transactions.Add(new Dictionary<string, dynamic>() {
                                { "success", t.Success },
                                { "date_time", t.DateTime },
                                { "from_card", t.FromCard },
                                { "to_card", t.ToCard },
                                { "amount", t.Amount },
                                { "currency", t.Currency }
                            });
                        }
                    }
                    if (db.Transactions.Where(t => t.ToCard == cardToken).Count() > 0)
                    {
                        var receivedTransactions = db.Transactions.Where(t => t.FromCard == cardToken).ToList();
                        foreach (var t in receivedTransactions)
                        {
                            transactions.Add(new Dictionary<string, dynamic>() {
                                { "success", t.Success },
                                { "date_time", t.DateTime },
                                { "from_card", t.FromCard },
                                { "to_card", t.ToCard },
                                { "amount", t.Amount },
                                { "currency", t.Currency }
                            });
                        }
                    }
                }

                return transactions;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
