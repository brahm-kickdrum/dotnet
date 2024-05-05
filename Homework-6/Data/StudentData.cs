using Homework_6.Model;

namespace Homework_6.Data
{
    public class StudentData
    {
        private static int _nextId = 1;
        public static readonly List<Student> Students = new List<Student>();

        public static int GetNextId()
        {
            return _nextId++;
        }
    }
}
