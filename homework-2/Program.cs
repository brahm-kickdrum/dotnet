using System;

namespace PersonalInformation
{
    public class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person();
            Person person2 = new Person("Brahm", 21);

            Console.WriteLine("Person 1");
            person1.DisplayInfo();
            Console.WriteLine("Person 2");
            person2.DisplayInfo();
        }
    }
}
