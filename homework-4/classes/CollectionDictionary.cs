using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionApplication
{
    class CollectionDictionary
    {
        private Dictionary<string, int> elements = new Dictionary<string, int>();

        public void Menu()
        {
            while (true)
            {
                Console.WriteLine("\nPlease enter a number from the given menu:");
                Console.WriteLine("1. Add an element to the Dictionary.");
                Console.WriteLine("2. Print all the elements present in the Dictionary.");
                Console.WriteLine("3. Delete the last element from the Dictionary.");
                Console.WriteLine("4. Delete the first element from the Dictionary.");
                Console.WriteLine("5. Delete the middle element from the Dictionary.");
                Console.WriteLine("6. Calculate the average of the elements present in the Dictionary.");
                Console.WriteLine("7. Go back to the previous menu.");
                Console.WriteLine("8. Exit the application.");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddElement();
                        break;
                    case 2:
                        PrintElements();
                        break;
                    case 3:
                        DeleteLastElement();
                        break;
                    case 4:
                        DeleteFirstElement();
                        break;
                    case 5:
                        DeleteMiddleElement();
                        break;
                    case 6:
                        CalculateAverage();
                        break;
                    case 7:
                        return;
                    case 8:
                        Console.WriteLine("Exiting the application...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number.");
                        break;
                }
            }
        }

        private void AddElement()
        {
            Console.WriteLine("Enter the key:");
            string key = Console.ReadLine();
            Console.WriteLine("Enter the value:");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                elements[key] = value;
                Console.WriteLine("Element added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
            }
        }

        private void PrintElements()
        {
            Console.WriteLine("Elements in the Dictionary:");
            foreach (var kvp in elements)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
            }
        }

        private void DeleteLastElement()
        {
            if (elements.Any())
            {
                var lastKey = elements.Keys.Last();
                elements.Remove(lastKey);
                Console.WriteLine("Last element deleted successfully.");
            }
            else
            {
                Console.WriteLine("Dictionary is empty. No elements to delete.");
            }
        }

        private void DeleteFirstElement()
        {
            if (elements.Any())
            {
                var firstKey = elements.Keys.First();
                elements.Remove(firstKey);
                Console.WriteLine("First element deleted successfully.");
            }
            else
            {
                Console.WriteLine("Dictionary is empty. No elements to delete.");
            }
        }

        private void DeleteMiddleElement()
        {
            if (elements.Any())
            {
                int middleIndex = elements.Count / 2;
                var middleKey = elements.Keys.ElementAt(middleIndex);
                elements.Remove(middleKey);
                Console.WriteLine("Middle element deleted successfully.");
            }
            else
            {
                Console.WriteLine("Dictionary is empty. No elements to delete.");
            }
        }

        private void CalculateAverage()
        {
            if (elements.Any())
            {
                double average = elements.Values.Average();
                Console.WriteLine($"Average of the elements in the Dictionary: {average}");
            }
            else
            {
                Console.WriteLine("Dictionary is empty. No elements to calculate average.");
            }
        }
    }
}