namespace ConsoleApp1
{
    internal class Market
    {
        public List<Buyer> buyers = new List<Buyer>();
        public List<Seller> sellers = new List<Seller>();

        public bool marketEvents = false;
        public void Simulate(int rounds)
        {
            Random random = new Random();

            for (int i = 0; i < rounds; i++)
            {
                Console.Clear();
                Console.WriteLine($"---{i + 1}. round---\n");

                if (marketEvents)
                {
                    if (random.Next(1, 101) < 25)
                    {
                        TriggerMarketEvent();
                    }
                }


                foreach (Seller seller in sellers)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(seller.name + "\n");
                    Thread.Sleep(150);


                    foreach (Product product in seller.products)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"{product.name} - {product.category} - {product.actualPrice.ToString("N2")}$ - {product.supply} units");
                        Thread.Sleep(150);

                    }

                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    foreach (Product product in seller.products)
                    {


                        int marketDemand = 0;

                        List<Buyer> potentialBuyers = buyers.Where(b => b.budget >= product.actualPrice).ToList();
                        for (int j = 0; j < potentialBuyers.Count(); j++)
                        {
                            if (potentialBuyers[j].budget >= product.actualPrice && random.Next(1, 101) < product.demand)

                            {
                                try
                                {
                                    buyers[j].buyProduct(product);
                                    Console.WriteLine($"\t{potentialBuyers[j].name} bought {product.name} for {product.actualPrice.ToString("N2")}$.\n");
                                    marketDemand++;
                                    Thread.Sleep(150);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message.ToString());
                                    Thread.Sleep(150);
                                }
                            }
                        }
                        try
                        {
                            //Console.WriteLine(marketDemand + " / " + product.supply);
                            double priceChange = product.adjustPriceAndDemand(marketDemand);
                            if (priceChange > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"The price of {product.name} went up {Math.Round(priceChange, 2).ToString("N2")}$. It now costs {product.actualPrice.ToString("N2")}$.\n");
                            }
                            else if (priceChange < 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The price of {product.name} went down {Math.Round(Math.Abs(priceChange), 2).ToString("N2")}$. It now costs {product.actualPrice.ToString("N2")}$.\n");
                            }
                            Console.ForegroundColor = ConsoleColor.White;

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString() + "\n");
                        }
                        Thread.Sleep(250);



                    }
                }

                Console.WriteLine($"\nEnd of round {i + 1}. Continue... (ENTER)");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorVisible = false;
                Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorVisible = true;

            }
        }


        private void Wait(int ms)
        {
            //if () ;
        }

        private void TriggerMarketEvent()
        {
            Random rnd = new Random();

            int eventCase = rnd.Next(1, 5);

            Console.ForegroundColor = ConsoleColor.Magenta;

            switch (eventCase)
            {
                case 1:
                    Console.WriteLine("The economy is booming. All buyer's have increased budgets (120%) and demand rises.\n");
                    foreach (Buyer buyer in buyers)
                    {
                        buyer.budget *= 1.2;
                    }
                    foreach (Seller seller in sellers)
                    {
                        foreach (Product product in seller.products)
                        {
                            product.demand += 10;
                        }

                    }

                    break;
                case 2:
                    Console.WriteLine("The economy is in a recession. All buyer's have decreased budgets (80%) and demand drops.\n");
                    foreach (Buyer buyer in buyers)
                    {
                        buyer.budget *= 0.8;
                    }
                    foreach (Seller seller in sellers)
                    {
                        foreach (Product product in seller.products)
                        {
                            product.demand -= 10;
                        }

                    }
                    break;
                case 3:
                    Console.WriteLine("Something happened with the supply chain. Prices increase due to reduced supply.\n");
                    foreach (Seller seller in sellers)
                    {
                        foreach (Product product in seller.products)
                        {
                            product.actualPrice *= 1.2;
                            product.supply -= product.supply / 4;
                        }

                    }
                    break;
                case 4:
                    Console.WriteLine("Every store has started a discount season. Prices drop.\n");
                    foreach (Seller seller in sellers)
                    {
                        foreach (Product product in seller.products)
                        {
                            product.actualPrice *= 0.8;
                        }

                    }
                    break;
            }



            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
