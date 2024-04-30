using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Collection Application!");

            while (true)
            {
                Console.WriteLine("\nPlease enter a number from the given menu:");
                Console.WriteLine("1. Continue the application using List.");
                Console.WriteLine("2. Continue the application using Dictionary.");
                Console.WriteLine("3. Exit the application.");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ListMenu();
                        break;
                    case 2:
                        DictionaryMenu();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the application...");
                        return;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
        }

        static void ListMenu()
        {
            var collection = new CollectionList();
            collection.Menu();
        }

        static void DictionaryMenu()
        {
            var collection = new CollectionDictionary();
            collection.Menu();
        }
    }
}
