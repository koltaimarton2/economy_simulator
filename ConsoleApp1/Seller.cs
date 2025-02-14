using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Seller
    {
        public string name { get; set; }
        public List<Product> products { get; set; }


        public Seller(string _name)
        {
            name = _name;
            products = new List<Product>();
        }

       public void addProduct(Product product)
        {
            products.Add(product);
        }

    }
}
