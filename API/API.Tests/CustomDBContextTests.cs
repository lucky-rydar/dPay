using API.Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using API;
using System.Diagnostics;
using System.IO;

namespace API.Tests
{
    [TestClass]
    public class CustomDBContextTests
    {
        [TestMethod]
        public void CanAddRemoveData()
        {
            try
            {
                CustomDBContext db = new CustomDBContext();
                db.Database.EnsureCreated();

                var card = new API.Card()
                {
                    Id = -1,
                    OwnerId = 1,
                    IsDefault = false,
                    Name = "Default card",
                    CardNumber = "1234123412341234",
                    CVV = "111",
                    ExpMonth = "13",
                    ExpYear = "23"
                };

                db.Cards.Add(card);
                db.SaveChanges();
                var card1 = db.Cards.Where(c => c.ExpMonth == "13").First();
                
                Assert.AreEqual(card.ExpMonth, card1.ExpMonth);
                Assert.AreEqual(card.Name, card1.Name);

                db.Cards.Remove(card);
                db.SaveChanges();

                Assert.ThrowsException<InvalidOperationException>(() => {
                    db.Cards.Where(c => c.ExpMonth == "13").First();
                });

                File.Delete("database.db");
            }
            catch(Exception e)
            {
                Trace.WriteLine(e.Message);
                Assert.Fail();
            }
        }
    }
}
