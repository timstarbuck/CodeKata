using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kata09_Checkout;
using System.Collections.Generic;

namespace Kata09_CheckoutTests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void WhenCartIsEmptyTotalIsZero()
        {
            Checkout co = new Checkout(GetPricing());
            Assert.AreEqual(co.Total(), 0);
        }

        [TestMethod]
        public void SingleItemIsPricedCorrectly()
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("A");
            Assert.AreEqual(co.Total(), 50);
        }

        [TestMethod]
        public void MultipleNonGroupPricedItemsTotalCorrectly()
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("A");
            co.Scan("B");
            co.Scan("C");
            co.Scan("D");
            Assert.AreEqual(co.Total(), 115);
        }

        [TestMethod]
        public void SingleGroupPricedItemsTotalCorrectly()
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            Assert.AreEqual(co.Total(), 130);
        }

        [TestMethod]
        public void MultipleGroupPricedItemsTotalCorrectly()
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("B");
            Assert.AreEqual(co.Total(), 210);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ScanItemNotInPriceListThrowsArgumentException()
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("Z");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConstructorRequiresPriceRules()
        {
            Checkout co = new Checkout(null);
        }

        private List<PriceRule> GetPricing()
        {
            List<PriceRule> rules = new List<PriceRule>();
            rules.Add(new PriceRule()
            {
                ItemSku = "A",
                ItemPrice = 50.0,
                GroupPrice = new GroupPriceRule()
                {
                    Quantity = 3,
                    GroupPrice = 130.0
                }
            });

            rules.Add(new PriceRule()
            {
                ItemSku = "B",
                ItemPrice = 30.0,
                GroupPrice = new GroupPriceRule()
                {
                    Quantity = 2,
                    GroupPrice = 45.0
                }
            });

            rules.Add(new PriceRule()
            {
                ItemSku = "C",
                ItemPrice = 20.0
            });

            rules.Add(new PriceRule()
            {
                ItemSku = "D",
                ItemPrice = 15.0
            });

            return rules;
        }
    }
}
