using System.ComponentModel;

namespace ConsoleApp1
{
    internal class Program
    {   
        private static List<string> options = new List<string>();
        private static List<string> sellerOptions = new List<string>();
        private static List<string> buyerOptions = new List<string>();
        static void Main(string[] args)
        {
            Menu();
        }


        static void Menu()
        {
            options = new List<string>
            {
                "Add new seller",
                "Add new buyer",
                "Simulate market",
            };

            sellerOptions = new List<string>
            {
                "Add name",
                "Add products"
            };

            buyerOptions = new List<string>
            {
                "Add name",
                "Add budget"
            };

            


            WriteMenu(options, options[0]);
            
            int index = 0;

            ConsoleKeyInfo keyinfo;

            do
            {
              
                keyinfo = Console.ReadKey();
                index = Move(keyinfo, index, options);
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine(index);
                    Console.ReadKey();
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

                                }

                                if (keyinfo.Key == ConsoleKey.LeftArrow)
                                {
                                    Console.Clear();
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
                }
                

                
            } while (keyinfo.Key != ConsoleKey.Escape);
        
        }

        




        static int Move(ConsoleKeyInfo keyinfo, int index, List<string> menu)
        {
            string selectedOption = menu[0];
            if (keyinfo.Key == ConsoleKey.DownArrow)
            {
                if (index + 1 < menu.Count)
                {
                    index++;
                    selectedOption = menu[index];
                    WriteMenu(menu, selectedOption);
                    return index;
                }
                return index;

            }

            if (keyinfo.Key == ConsoleKey.UpArrow)
            {
                if (index - 1 > -1)
                {
                    index--;
                    selectedOption = menu[index];
                    WriteMenu(menu, selectedOption);
                    return index;
                }
                return 0;
            }
            else return 0;

        }

        static void WriteMenu(List<string> menu, string selectedOption)
        {
            Console.Clear();

            foreach (string option in menu)
            {
                if (option == selectedOption)
                {
                    //Console.BackgroundColor = ConsoleColor.White;
                    //Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("> " + option);
                }
                else
                {
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" " + option);
                }
            }
        }
    }
}
