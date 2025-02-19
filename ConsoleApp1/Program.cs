namespace ConsoleApp1
{

    internal class Menu
    {
        public string Caption { get; set; }

        public string? Title { get; set; }
        public List<Menu>? Items { get; set; }
        public Menu parentMenu { get; set; }
        public int parentIndex { get; set; }

        public Action? MenuHandler { get; set; }

        public Menu(string Caption = "", List<Menu>? Items = null, Action? EventHandler = null, string? Title = null)
        {
            this.Caption = Caption;
            this.Items = Items;
            MenuHandler = EventHandler;
            this.Title = Title;
        }
    }

    internal class Program
    {
        private static Menu options = new Menu();
        private static List<Menu> sellerOptions = new List<Menu>();
        private static List<Menu> buyerOptions = new List<Menu>();
        private static List<Menu> fillOptions = new List<Menu>();
        private static List<Menu> Options = new List<Menu>();

        private static string entityName = null;
        private static string productName = null;
        private static string entityCategory = null;
        private static double entityBudget = -1.0;
        private static int entitySupply = -1;
        private static List<Product> entityProducts = new List<Product>();

        public static Market market = new Market();
        static void Main(string[] args)
        {
            Menu();
        }




        static void SetBuyerName()
        {
            Console.Clear();

            do
            {
                Console.Write("Buyer's name: ");
                entityName = Console.ReadLine();
                if (string.IsNullOrEmpty(entityName) || entityName.Any(char.IsDigit))
                {
                    Console.WriteLine("Invalid name. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"{entityName}'s name set. Continue...");
                    Console.ReadKey();
                }

            } while (string.IsNullOrEmpty(entityName));


        }
        static void AddBudget()
        {
            Console.Clear();

            bool exit = false;

            do
            {
                if (entityName != null)
                {
                    Console.Write($"{entityName}'s budget ($): ");
                }
                else
                {
                    Console.Write("Buyer's budget ($): ");
                }
                if (Double.TryParse(Console.ReadLine(), out entityBudget) && entityBudget > 0)
                {
                    exit = true;
                    Console.WriteLine("Buyer's budget set. Continue...");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Invalid budget. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (exit == false);
        }

        static void CreateBuyer()
        {
            Console.Clear();
            if (entityName == null)
            {
                Console.WriteLine("Buyer needs a name.");
                Console.ReadKey();
                return;
            }
            else if (entityBudget == -1.0)
            {
                Console.WriteLine("Buyer needs a budget.");
                Console.ReadKey();
                return;
            }
            else
            {
                market.buyers.Add(new Buyer(entityName, entityBudget));
                Console.WriteLine($"{entityName} added with a budget of {entityBudget}$.");
                Console.ReadKey();
                entityName = null;
                entityBudget = -1.0;
                return;
            }
        }

        static void SetSellerName()
        {
            Console.Clear();

            do
            {
                Console.Write("Seller's name: ");
                entityName = Console.ReadLine();
                if (IsCorrect(entityName) == false)
                {
                    Console.WriteLine("Invalid name. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"{entityName}'s name set. Continue...");
                    Console.ReadKey();
                }

            } while (string.IsNullOrEmpty(entityName));

        }

        static void AddSellerProducts()
        {
            Console.Clear();

            bool exit = false;

            while (!exit)
            {
                Console.Clear();

                exit = true;

                if (string.IsNullOrEmpty(productName))
                {
                    Console.Write("Product's name: ");
                    productName = Console.ReadLine();
                    if (!IsCorrect(productName))
                    {
                        Console.WriteLine("Invalid name. Try again...");
                        productName = null;
                        Console.ReadKey();
                        exit = false;
                        continue;
                    }
                }

                if (string.IsNullOrEmpty(entityCategory))
                {
                    Console.Write("Product's category: ");
                    entityCategory = Console.ReadLine();
                    if (!IsCorrect(entityCategory))
                    {
                        Console.WriteLine("Invalid category. Try again...");
                        entityCategory = null;
                        Console.ReadKey();
                        exit = false;
                        continue;
                    }
                }

                if (entityBudget == -1.0)
                {
                    Console.Write("Product's base price ($): ");
                    if (!double.TryParse(Console.ReadLine(), out entityBudget) || entityBudget < 0)
                    {
                        Console.WriteLine("Invalid base price. Try again...");
                        entityBudget = -1.0;
                        Console.ReadKey();
                        exit = false;
                        continue;
                    }
                }

                if (entitySupply == -1)
                {
                    Console.Write("Product's supply (unit): ");
                    if (!int.TryParse(Console.ReadLine(), out entitySupply) || entitySupply < 0)
                    {
                        Console.WriteLine("Invalid supply. Try again...");
                        entitySupply = -1;
                        Console.ReadKey();
                        exit = false;
                        continue;
                    }
                }


            }
            entityProducts.Add(new Product(productName, entityBudget, entityCategory, entitySupply));
            Console.WriteLine($"{productName} added to seller's products.");
            productName = null;
            entityCategory = null;
            entitySupply = -1;
            entityBudget = -1.0;
        }

        static void CreateSeller()
        {
            Console.Clear();
            if (entityName == null)
            {
                Console.WriteLine("Seller needs a name.");
                Console.ReadKey();
                return;
            }
            else if (entityProducts.Count() == 0)
            {
                Console.WriteLine("Seller needs at least one product.");
                Console.ReadKey();
                return;
            }
            else
            {
                market.sellers.Add(new Seller(entityName, entityProducts));
                Console.WriteLine($"{entityName} added with {entityProducts.Count()} products.");
                Console.ReadKey();
                entityName = null;

                entityProducts.Clear();
                return;
            }
        }

        static void FillUpMarketSmall()
        {
            Seller Bakery = new Seller("Bakery", new List<Product>
            {
                new Product("Bread", 1.50, "Bakery", 30),
                new Product("Croissant", 2.00, "Bakery", 20),
                new Product("Bagel", 1.75, "Bakery", 25)
            });

            Seller Grocery = new Seller("Grocery Store", new List<Product>
            {
                new Product("Apple", 0.75, "Fruits", 50),
                new Product("Banana", 0.50, "Fruits", 60),
                new Product("Carrot", 0.40, "Vegetables", 40)
            });

            Seller Electronics = new Seller("Electronics Shop", new List<Product>
            {
                new Product("Headphones", 25.99, "Electronics", 15),
                new Product("Mouse", 15.50, "Electronics", 25),
                new Product("Keyboard", 30.00, "Electronics", 10)
            });

            Seller Clothing = new Seller("Clothing Store", new List<Product>
            {
                new Product("T-Shirt", 10.00, "Clothing", 50),
                new Product("Jeans", 35.00, "Clothing", 30),
                new Product("Jacket", 60.00, "Clothing", 20)
            });


            Buyer Adrian = new Buyer("Adrian", 1000);
            Buyer Sophia = new Buyer("Sophia", 250);
            Buyer Michael = new Buyer("Michael", 800);
            Buyer Emma = new Buyer("Emma", 500);
            Buyer James = new Buyer("James", 6000);

            market.sellers.Add(Bakery);
            market.sellers.Add(Grocery);
            market.sellers.Add(Electronics);
            market.sellers.Add(Clothing);

            market.buyers.Add(Adrian);
            market.buyers.Add(Sophia);
            market.buyers.Add(Michael);
            market.buyers.Add(Emma);
            market.buyers.Add(James);

            Console.WriteLine($"\nMarket filled up with {market.sellers.Count()} sellers and {market.buyers.Count()} buyers.");
            Console.ReadKey();

        }

        static void FillUpMarketBig()
        {
            Seller Bookstore = new Seller("Bookstore", new List<Product>
            {
                new Product("Novel", 12.99, "Books", 100),
                new Product("Textbook", 40.00, "Books", 20),
                new Product("Magazine", 5.00, "Books", 50)
            });

                        Seller Furniture = new Seller("Furniture Shop", new List<Product>
            {
                new Product("Sofa", 250.00, "Furniture", 10),
                new Product("Dining Table", 180.00, "Furniture", 15),
                new Product("Bookshelf", 80.00, "Furniture", 25)
            });

                        Seller ToyStore = new Seller("Toy Store", new List<Product>
            {
                new Product("Action Figure", 10.50, "Toys", 40),
                new Product("Lego Set", 45.00, "Toys", 30),
                new Product("Doll", 12.00, "Toys", 60)
            });

                        Seller Pharmacy = new Seller("Pharmacy", new List<Product>
            {
                new Product("Pain Reliever", 7.00, "Health", 100),
                new Product("Vitamins", 15.00, "Health", 50),
                new Product("Bandages", 3.50, "Health", 150)
            });

                        Seller SportsStore = new Seller("Sports Store", new List<Product>
            {
                new Product("Basketball", 20.00, "Sports", 25),
                new Product("Soccer Ball", 15.00, "Sports", 30),
                new Product("Yoga Mat", 25.00, "Sports", 10)
            });

            Buyer Olivia = new Buyer("Olivia", 750);
            Buyer Liam = new Buyer("Liam", 200);
            Buyer Noah = new Buyer("Noah", 1000);
            Buyer Isabella = new Buyer("Isabella", 1500);
            Buyer Lucas = new Buyer("Lucas", 500);
            Buyer Ava = new Buyer("Ava", 1200);
            Buyer Mason = new Buyer("Mason", 350);
            Buyer Ella = new Buyer("Ella", 900);
            Buyer Jack = new Buyer("Jack", 150);
            Buyer Grace = new Buyer("Grace", 1800);
            Buyer Henry = new Buyer("Henry", 600);
            Buyer Chloe = new Buyer("Chloe", 800);
            Buyer Alexander = new Buyer("Alexander", 1300);
            Buyer Sofia = new Buyer("Sofia", 400);
            Buyer Benjamin = new Buyer("Benjamin", 2500);


            market.sellers.Add(Bookstore);
            market.sellers.Add(Furniture);
            market.sellers.Add(ToyStore);
            market.sellers.Add(Pharmacy);
            market.sellers.Add(SportsStore);

            market.buyers.Add(Olivia);
            market.buyers.Add(Liam);
            market.buyers.Add(Noah);
            market.buyers.Add(Isabella);
            market.buyers.Add(Lucas);
            market.buyers.Add(Ava);
            market.buyers.Add(Mason);
            market.buyers.Add(Ella);
            market.buyers.Add(Jack);
            market.buyers.Add(Grace);
            market.buyers.Add(Henry);
            market.buyers.Add(Chloe);
            market.buyers.Add(Alexander);
            market.buyers.Add(Sofia);
            market.buyers.Add(Benjamin);

            Console.WriteLine($"\nMarket filled up with {market.sellers.Count()} sellers and {market.buyers.Count()} buyers.");
            Console.ReadKey();
        }

        static void GiveMoney()
        {
            Console.Clear();

            bool exit = false;

            double money;

            do
            {
                Console.Write("Money to give ($): ");

                if (Double.TryParse(Console.ReadLine(), out money) && money > 0)
                {
                    exit = true;
                    Console.WriteLine($"{money}$ given to every buyer. Continue...");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Invalid amount. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (exit == false);

            foreach (Buyer buyer in market.buyers)
            {
                buyer.budget += money;
            }
        }

        static void ListBuyers()
        {
            Console.Clear();
            foreach (Buyer buyer in market.buyers)
            {
                Console.WriteLine($"{buyer.name} - {buyer.budget}$");
            }
            Console.ReadKey();
        }

        static void FillUpSupply()
        {
            Console.Clear();

            bool exit = false;

            int supply;

            do
            {
                Console.Write("Units to give: ");

                if (int.TryParse(Console.ReadLine(), out supply) && supply > 0)
                {
                    exit = true;
                    Console.WriteLine($"{supply} units given to every seller for every product. Continue...");
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Invalid amount. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (exit == false);

            foreach (Seller seller in market.sellers)
            {
                foreach (Product product in seller.products)
                {
                    product.supply += supply;
                }
            }
        }

        static void ListSellers()
        {
            Console.Clear();
            foreach (Seller seller in market.sellers)
            {
                Console.WriteLine($"\n{seller.name}");
                Console.WriteLine("Products:\n");
                foreach (Product product in seller.products)
                {
                    Console.WriteLine($"\t{product.name} - {product.category} - {product.actualPrice}$ - {product.supply} units");
                }
            }
            Console.ReadKey();
        }

        static void EnableEvents()
        {
            if (market.marketEvents == false)
            {
                market.marketEvents = true;
                Console.WriteLine("\nSudden market events enabled.");
                Console.ReadKey();
            }
            else
            {
                market.marketEvents = false;
                Console.WriteLine("\nSudden market events disabled.");
                Console.ReadKey();
            }
        }

        static void SimulateMarket()
        {
            Console.Clear();

            bool exit = false;
            int rounds = 0;

            do
            {
                Console.Write("Simulation rounds: ");
                if (int.TryParse(Console.ReadLine(), out rounds) || rounds > 0)
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid round number. Try again...");
                    Console.ReadKey();
                    Console.Clear();
                }
            } while (exit == false);

            Console.Clear();

            market.Simulate(rounds);

        }

        static void Menu()
        {
            sellerOptions = new List<Menu> {
                new Menu("Set name", null, SetSellerName, "Seller menu"),
                new Menu("Set products", null, AddSellerProducts),
                new Menu("Create seller\n", null, CreateSeller),
                new Menu("Fill up every supply", null, FillUpSupply),
                new Menu("List all sellers", null, ListSellers)
            };

            buyerOptions = new List<Menu> {
                new Menu("Set name", null, SetBuyerName, "Buyer menu"),
                new Menu("Set budget", null, AddBudget),
                new Menu("Create buyer\n",null, CreateBuyer),
                new Menu("Give every buyer money", null, GiveMoney),
                new Menu("List all buyers", null, ListBuyers)
            };

            fillOptions = new List<Menu>
            {
                new Menu("Small market", null, FillUpMarketSmall, "Market menu"),
                new Menu("Big market", null, FillUpMarketBig)
            };

            options = new Menu("", new List<Menu> {
                new Menu("Add new seller", sellerOptions,null, "Main menu"),
                new Menu("Add new buyer", buyerOptions, null),
                new Menu("Fill up market", fillOptions, null),
                new Menu("Sudden market events OFF/ON", null, EnableEvents),
                new Menu("Simulate market", null, SimulateMarket)
            }, null, ""); ;

            int index = 0;
            Menu currentMenu = options;

            WriteMenu(currentMenu, currentMenu.Items[index]);


            ConsoleKeyInfo keyinfo;
            ConsoleKey currentKey;

            do
            {
                if (currentMenu == null) break;

                keyinfo = Console.ReadKey();
                currentKey = keyinfo.Key;

                index = Move(keyinfo, index, currentMenu);
                switch (currentKey)
                {
                    case ConsoleKey.Enter:
                        {
                            if (currentMenu.Items[index].MenuHandler != null)
                            {
                                currentMenu.Items[index].MenuHandler();
                                WriteMenu(currentMenu, currentMenu.Items[index]);
                            }
                            else
                            if (currentMenu.Items[index].Items != null)
                            {
                                currentMenu.Items[index].parentMenu = currentMenu;
                                currentMenu.Items[index].parentIndex = index;
                                currentMenu = currentMenu.Items[index];
                                index = 0;
                                if (currentMenu != null)
                                    WriteMenu(currentMenu, currentMenu.Items[index]);
                            }
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentMenu.parentMenu != null)
                            {
                                int idx = currentMenu.parentIndex;
                                currentMenu = currentMenu.parentMenu;
                                index = idx;
                                if (currentMenu != null)
                                    WriteMenu(currentMenu, currentMenu.Items[index]);
                                currentKey = ConsoleKey.NoName;
                            }
                            break;
                        }
                }

            } while (currentKey != ConsoleKey.Escape);

        }






        static int Move(ConsoleKeyInfo keyinfo, int index, Menu menu)
        {
            if (menu.Items == null) return 0;
            Menu selectedOption = menu.Items[index];
            switch (keyinfo.Key)
            {
                case ConsoleKey.DownArrow:
                    {
                        if (index + 1 < menu.Items.Count)
                        {
                            index++;
                            selectedOption = menu.Items[index];
                            WriteMenu(menu, selectedOption);

                        }
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (index - 1 > -1)
                        {
                            index--;
                            selectedOption = menu.Items[index];
                            WriteMenu(menu, selectedOption);
                        }
                        break;
                    }

            }

            return index;
        }

        static void WriteMenu(Menu menu, Menu selectedOption)
        {
            if (menu.Items == null) return;
            Console.Clear();

            Console.WriteLine($"{menu.Items[0].Title}\n");
            foreach (Menu option in menu.Items)
            {
                if (option == selectedOption)
                {
                    Console.WriteLine("> " + option.Caption);
                }
                else
                {
                    Console.WriteLine(" " + option.Caption);
                }
            }
        }

        static bool IsCorrect(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Any(char.IsDigit))
            {
                return false;
            }
            return true;
        }
    }
}
