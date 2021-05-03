using API.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using LiqPay.Protocols;
using LiqPay.Models;
using LiqPay.Models.Requests;

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

        [HttpGet("send_by_card/{token}/{from_card}/{to_card}/{amount}/{currency}")]
        public Dictionary<string, dynamic> SendByCard(string token, string from_card, string to_card, int amount, string currency)
        {
            try
            {
                Dictionary<string, dynamic> res = new Dictionary<string, dynamic>();

                var userFrom = db.Users.Where(u => u.Token == token).FirstOrDefault();

                int orderID = 1;
                try
                {
                    orderID = db.Transactions.Max(t => t.Id) + 1;
                }
                catch (Exception)
                {

                }

                Card card = db.Cards.Where(c => c.OwnerId == userFrom.Id && c.CardNumber == from_card).FirstOrDefault();

                Currency c = new Currency();
                switch (currency)
                {
                    case "UAH":
                        c = Currency.UAH;
                        break;
                    case "USD":
                        c = Currency.USD;
                        break;
                    case "EUR":
                        c = Currency.EUR;
                        break;
                    default:
                        throw new Exception("invalid currency");
                }

                var protocol = new P2PLiqPayProtocol(LiqpayKeys.PublicKey, LiqpayKeys.PrivateKey);
                var response = protocol.P2P(new P2PLiqPayRequestModel()
                {
                    Action = "p2p",
                    Version = 3,
                    Phone = userFrom.Phone,
                    Amount = amount,
                    Currency = c,
                    Description = "", // make description available for method
                    OrderId = (orderID).ToString() + "_order",
                    ReceiverCard = to_card,
                    Card = from_card,
                    CardCvv = card.CVV,
                    CardExpMonth = card.ExpMonth,
                    CardExpYear = card.ExpYear
                });
                bool paySuccess = (response != null);

                res.Add("order_id", (orderID).ToString() + "_order");
                res.Add("success", paySuccess);
                res.Add("currency", currency);
                res.Add("amount", amount);

                DateTime now = DateTime.Now;
                Transaction t = new Transaction()
                {
                    DateTime = now.ToString(),
                    Success = paySuccess,
                    FromCard = from_card,
                    ToCard = to_card,
                    FromUser = "",
                    ToUser = "",
                    Amount = amount,
                    Currency = currency
                };
                db.Transactions.Add(t);
                db.SaveChanges();

                return res;
            }
            catch (Exception e)
            {
                return new Dictionary<string, dynamic>()
                {
                    { "success", false },
                    { "order_id", (-1).ToString()+"_order" },
                    { "currency", currency },
                    { "amount", amount }
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
                    var number = card.CardNumber;
                    if(db.Transactions.Where(t=>t.FromCard == number).Count() > 0)
                    {
                        var sentTransactions = db.Transactions.Where(t => t.FromCard == number).ToList();
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
                    if (db.Transactions.Where(t => t.ToCard == number).Count() > 0)
                    {
                        var receivedTransactions = db.Transactions.Where(t => t.FromCard == number).ToList();
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
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
