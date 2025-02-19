namespace ConsoleApp1
{
    internal class Buyer
    {
        public string name { get; set; }
        public double budget { get; set; }

        public Buyer(string _name, double _budget)
        {
            name = _name;
            budget = _budget;
        }

        public void buyProduct(Product product)
        {
            if (budget - product.actualPrice >= 0)
            {
                budget -= product.actualPrice;
                product.supply--;
            }
            else
            {
                throw new Exception($"\t{name} doesn't have enough money.\n");
            }
        }


    }
}
