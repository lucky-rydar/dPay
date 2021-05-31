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
            int orderId = 1;
            try
            {
                orderId = db.Transactions.Max(t => t.Id) + 1;
            }
            catch (Exception) { }

            string cur = "";

            try
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

                var userFrom = db.Users.Where(u => u.Token == token).FirstOrDefault();
                var fromCard = db.Cards.Where(c => c.CardToken == from_card).FirstOrDefault();
                cur = fromCard.Currency;

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

                cur = fromCard.Currency;

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
                db.Transactions.Add(new Transaction()
                {
                    Id = orderId,
                    Success = false,
                    DateTime = DateTime.Now.ToString(),
                    FromCard = from_card,
                    ToCard = to_card,
                    Amount = amount,
                    Currency = cur
                });

                db.SaveChanges();

                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "order_id", orderId },
                    { "amount", amount }
                };
            }
        }

        [HttpGet("send_by_username/{token}/{from_username}/{to_username}/{amount}")]
        public Dictionary<string, dynamic> SendByUsername(string token, string from_username, string to_username, float amount) // todo save in database if even failure
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
        public List<Transaction> Transactions(string token)
        {
            try
            {
                var cardIds = new List<int>();
                var ownerId = db.Users.Where(u => u.Token == token).FirstOrDefault().Id;
                var cards = db.Cards.Where(c => c.OwnerId == ownerId).ToList();
                List<Transaction> transactions = new List<Transaction>();
                
                foreach(var card in cards)
                {
                    var cardToken = card.CardToken;
                    if(db.Transactions.Where(t=>t.FromCard == cardToken).Count() > 0)
                    {
                        var sentTransactions = db.Transactions.Where(t => t.FromCard == cardToken).ToList();
                        foreach(var t in sentTransactions)
                        {
                            transactions.Add(new Transaction() {
                                Id = t.Id,
                                Success = t.Success,
                                DateTime = t.DateTime,
                                FromCard = t.FromCard,
                                ToCard = t.ToCard,
                                Amount = t.Amount,
                                Currency = t.Currency
                            });
                        }
                    }
                    if (db.Transactions.Where(t => t.ToCard == cardToken).Count() > 0)
                    {
                        var receivedTransactions = db.Transactions.Where(t => t.FromCard == cardToken).ToList();
                        foreach (var t in receivedTransactions)
                        {
                            transactions.Add(new Transaction()
                            {
                                Id = t.Id,
                                Success = t.Success,
                                DateTime = t.DateTime,
                                FromCard = t.FromCard,
                                ToCard = t.ToCard,
                                Amount = t.Amount,
                                Currency = t.Currency
                            });
                        }
                    }
                }
                
                return transactions.Distinct(new TransactionComparer()).ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }

        private class TransactionComparer : IEqualityComparer<Transaction>
        {
            public bool Equals(Transaction t1, Transaction t2)
            {
                return t1.Id == t2.Id;
            }

            public int GetHashCode(Transaction obj) 
            { 
                return obj.Id.GetHashCode(); 
            }
        }
    }

    
}
