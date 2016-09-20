using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata09_Checkout
{
    class Program
    {
        static void Main(string[] args)
        {
            Checkout co = new Checkout(GetPricing());
            co.Scan("A");
            co.Scan("A");
            co.Scan("A");
            co.Scan("B");
            Console.WriteLine(co.Total());

            Console.ReadLine();
        }

        private static List<PriceRule> GetPricing()
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
