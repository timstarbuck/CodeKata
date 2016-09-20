using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata09_Checkout
{
    public class Checkout
    {
        private Dictionary<string, PriceRule> _priceRules;
        private Dictionary<string, int> _cart;

        public Checkout(List<PriceRule> priceRules)
        {
            if (priceRules == null)
            {
                throw new ArgumentNullException("priceRules");
            }
            _priceRules = new Dictionary<string, PriceRule>();
            foreach (var i in priceRules)
            {
                if (_priceRules.ContainsKey(i.ItemSku) == false)
                {
                    _priceRules.Add(i.ItemSku, i);
                }
            }
            _cart = new Dictionary<string, int>();
        }

        public void Scan(string itemSku)
        {
            if (_priceRules.ContainsKey(itemSku) == false)
            {
                throw new InvalidOperationException($"Item {itemSku} not found in price rules!");
            }
            if (_cart.ContainsKey(itemSku))
            {
                _cart[itemSku] += 1;
            }
            else
            {
                _cart.Add(itemSku, 1);
            }
        }

        public double Total()
        {
            double total = 0;
            foreach (var i in _cart)
            {
                PriceRule rule;
                if (_priceRules.TryGetValue(i.Key, out rule))
                {
                    int quantity = i.Value;
                    if (rule.GroupPrice != null)
                    {
                        total += CalculateGroupPrice(rule, ref quantity);
                    }
                    total += (quantity * rule.ItemPrice);
                }
            }
            return total;
        }

        private double CalculateGroupPrice(PriceRule rule, ref int quantity)
        {
            double total = 0;
            while (quantity >= rule.GroupPrice.Quantity)
            {
                total += rule.GroupPrice.GroupPrice;
                quantity -= rule.GroupPrice.Quantity;
            }
            return total;
        }
    }


}
