using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Product
    {
        public string name { get; private set; }
        public double basePrice { get; set; }

        public double actualPrice { get; set; }
        public string category { get; set; }
        public int supply { get; set; }

        public Product(string _name, double _basePrice, string _category, int _supply)
        {
            name = _name;
            basePrice = _basePrice;
            actualPrice = basePrice;
            category = _category;
            supply = _supply;
        }
        public void determineDemand(int demand)
        {
            if (demand > supply)
            {
                actualPrice *= 1.1;
            }
            else if (demand < actualPrice)
            {
                actualPrice *= 0.9;
            }
            else
            {
                throw new Exception("Price stayed the same.");
            }


        }
    }
}
