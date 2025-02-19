﻿namespace ConsoleApp1
{
    internal class Seller
    {
        public string name { get; set; }
        public List<Product> products { get; set; }


        public Seller(string _name, List<Product> _products)
        {
            name = _name;
            products = _products;
        }

        public void addProduct(Product product)
        {
            products.Add(product);
        }

    }
}
