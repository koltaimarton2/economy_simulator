namespace ConsoleApp1
{
    internal class Program
    {   
        private static List<string> options = new List<string>();
        static void Main(string[] args)
        {
            options = new List<string>
            {
                "Add new seller",
                "Add new buyer",
                "Simulate market",
                "Exit"
            };

            string selectedOption = options[0];

            int index = 0;

            WriteMenu(options, selectedOption);

            ConsoleKeyInfo keyinfo;
            
             do
            {
                keyinfo = Console.ReadKey();
                
                
                if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        selectedOption = options[index];
                        WriteMenu(options, selectedOption);
                    }
                    
                }
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    if (index - 1 > -1)
                    {
                        index--;
                        selectedOption = options[index];
                        WriteMenu(options, selectedOption);
                    }
                }
            }while (keyinfo.Key != ConsoleKey.Escape);
        }


        static void WriteMenu(List<string> menu, string selectedOption)
        {
            Console.Clear();

            foreach (string option in options)
            {
                if (option == selectedOption)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("> " + option);
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(" " + option);
                }
                Console.WriteLine(selectedOption);
            }
        }
    }
}
