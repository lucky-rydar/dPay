using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Database;

namespace API.Tests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void ValidCardTest()
        {
            try
            {
                string validCard = "5169360007906534";
                string notValidCard = "6583761234567890";

                Assert.AreEqual(Card.CardValid(validCard), true);
                Assert.AreEqual(Card.CardValid(notValidCard), false);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}
