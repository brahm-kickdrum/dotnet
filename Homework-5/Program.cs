using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegerManipulation
{
    class Program
    {
        static void Main()
        {
            List<int> numbers = Enumerable.Range(1, 10).ToList();

            List<int> squaredNumbers = numbers.Select(x => x * x).ToList();

            List<int> evenNumbers = numbers.Where(x => x % 2 == 0).ToList();

            int sumOfEvenNumbers = evenNumbers.Sum();

            Console.WriteLine("Original List:");
            PrintList(numbers);

            Console.WriteLine("\nSquared List:");
            PrintList(squaredNumbers);

            Console.WriteLine("\nFiltered List (Even Numbers Only):");
            PrintList(evenNumbers);

            Console.WriteLine("\nSum of Even Numbers: " + sumOfEvenNumbers);
        }

        static void PrintList(List<int> list)
        {
            foreach (int num in list)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
        }
    }
}
