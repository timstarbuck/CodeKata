using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata09_Checkout
{
    public class PriceRule
    {
        public string ItemSku { get; set; }

        public double ItemPrice { get; set; }

        public GroupPriceRule GroupPrice { get; set; }
    }
}
