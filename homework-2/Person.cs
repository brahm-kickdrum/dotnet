using System;

namespace PersonalInformation
{
    public class Person
    {
        private string _name;
        private int _age;

        public Person()
        {
            _name = "Unknown";
            _age = 0;
        }

        public Person(string name, int age)
        {
            _name = name;
            _age = age;
        }

        ~Person()
        {
            Console.WriteLine($"The object {_name} is being destroyed.");
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {_name}, Age: {_age}");
        }
    }
}