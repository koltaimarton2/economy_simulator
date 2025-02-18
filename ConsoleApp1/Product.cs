using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Product
    {
        public double priceStep = 0.15;
        public string name { get; private set; }

        public double basePrice { get; set; }

        public double actualPrice { get; set; }
        public string category { get; set; }
        public int supply { get; set; }

        public int demand { get; set; }

        public Product(string _name, double _basePrice, string _category, int _supply)
        {
            name = _name;
            basePrice = _basePrice;
            actualPrice = basePrice;
            category = _category;
            supply = _supply;
            demand = 50;
        }
        public double adjustPriceAndDemand(int marketDemand)
        {
            double lastPrice = actualPrice;
            if (marketDemand > supply)
            {
                actualPrice *= 1 + priceStep;
                if (demand <= 100)
                demand++;
                return actualPrice - lastPrice;

            }
            else if (marketDemand < supply)
            {
                actualPrice *= 1 - priceStep;
                if (demand > 0)
                demand--;
                return actualPrice - lastPrice;
            }
            else
            {
                throw new Exception("\nPrice stayed the same.");
            }


        }
    }
}
