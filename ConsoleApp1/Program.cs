using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{

    internal class Menu
    {
        public string Caption { get; set; }
        public List<Menu>? Items { get; set; }
        public Menu parentMenu { get; set; }
        public int parentIndex { get; set; }

        public Action? MenuHandler { get; set; }

        public Menu(string Caption = "", List<Menu>? Items = null, Action? EventHandler = null )
        {
            this.Caption = Caption; 
            this.Items = Items; 
            MenuHandler = EventHandler;  
        }
    }

    internal class Program
    {
        //private static List<string> options = new List<string>();
        //private static List<string> sellerOptions = new List<string>();
        //private static List<string> buyerOptions = new List<string>();
        //private static List<MenuItem> mainMenu = new List<MenuItem>();
        private static Menu options = new Menu();
        private static List<Menu> sellerOptions = new List<Menu>();
        private static List<Menu> buyerOptions = new List<Menu>();
        private static List<Menu> Options = new List<Menu>();

        public static Market market = new Market();
        static void Main(string[] args)
        {
            Menu();
        }

        static void SetBuyerName()
        {
            Console.Clear();

            string name = "";


            do
            {
                Console.Write("Buyer's name: ");
                try
                {
                    name = Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("Incorrect format.");
                }

            } while (name == "" );


            //market.buyers.Add(new Buyer(name, budget));

        }
        static void AddBudget()
        {
            double budget = -1.0;
            do
            {
            Console.Write("Buyer's budget: ");
            try
            {
                budget = Double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Incorrect format.");
            }
            }while (budget == -1.0);
        }

        static void Menu()
        {
            sellerOptions =new List<Menu> {
                new Menu("Add name"),
                new Menu("Add products")
            };

            buyerOptions = new List<Menu> {
                new Menu("Add name", null, SetBuyerName),
                new Menu("Add budget", null, AddBudget)
            };

            options = new Menu("", new List<Menu> {
                new Menu("Add new seller", sellerOptions),
                new Menu("Add new buyer", buyerOptions),
                new Menu("Simulate market"),
            });

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
                switch(currentKey)
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

                /*if (keyinfo.Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    switch (index)
                    {
                        case 0:

                            WriteMenu(sellerOptions, sellerOptions[0]);
                            do
                            {
                                keyinfo = Console.ReadKey();
                                index = Move(keyinfo, index, sellerOptions);

                                if (keyinfo.Key == ConsoleKey.Enter)
                                {
                                    Console.Clear();
                                    string title = "";
                                    string name = " ";
                                    string category = " ";
                                    double basePrice = -1;
                                    int supply = -1;
                                    List<Product> products = new List<Product>();
                                    switch (index)
                                    {
                                        case 0:
                                            Console.Write("Seller's name: ");
                                            title = Console.ReadLine();
                                            
                                            break;
                                        case 1:
                                            

                                            do
                                            {
                                                Console.Write("Product's name: ");
                                                name = Console.ReadLine();
                                                Console.Write($"{name}'s category: ");
                                                category = Console.ReadLine() ;
                                                Console.Write($"{name}'s base price: ");
                                                basePrice = Double.Parse(Console.ReadLine());
                                                Console.Write($"{name}'s supply: ");
                                                supply = int.Parse(Console.ReadLine());

                                                Product product = new Product(name,basePrice, category, supply);
                                                products.Add(product);

                                            } while (name != "" && category != "" && basePrice != -1 && supply != -1);
                                            break;
                                    }
                                    Seller seller = new Seller(title);
                                    seller.products = products;
                                    market.sellers.Add(seller);
                                }

                                if (keyinfo.Key == ConsoleKey.LeftArrow)
                                {
                                    Console.Clear();
                                    index = 0;
                                    WriteMenu(options, options[0]);
                                    break;
                                }

                            } while (true);

                            break;
                        case 1:
                            WriteMenu(buyerOptions, buyerOptions[0]);
                            do
                            {
                                keyinfo = Console.ReadKey();
                                index = Move(keyinfo, index, buyerOptions);

                                if (keyinfo.Key == ConsoleKey.LeftArrow)
                                {
                                    Console.Clear();
                                    WriteMenu(options, options[0]);
                                    break;
                                }

                            } while (true);

                            break;
                    }
                }*/



            } while (currentKey != ConsoleKey.Escape);
        
        }

        




        static int Move(ConsoleKeyInfo keyinfo, int index, Menu menu)
        {
            if (menu.Items == null) return 0;
            Menu selectedOption = menu.Items[index];
            switch(keyinfo.Key)
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
            foreach (Menu option in menu.Items)
            {
                if (option == selectedOption)
                {
                    //Console.BackgroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("> " + option.Caption);
                }
                else
                {
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" " + option.Caption);
                }
            }
        }
    }
}
