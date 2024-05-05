using Homework_6.Data;
using Homework_6.Dto;
using Homework_6.Model;
using Microsoft.AspNetCore.Mvc;

namespace Homework_6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpPost]
        public ActionResult<int> CreateStudent([FromBody] StudentRequestDto studentRequest)
        {
            var student = new Student
            {
                Name = studentRequest.Name,
                Age = studentRequest.Age,
                Grade = studentRequest.Grade
            };

            student.Id = StudentData.GetNextId();
            StudentData.Students.Add(student);
            return student.Id;
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = StudentData.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            return student;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetAllStudents()
        {
            return StudentData.Students;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, StudentRequestDto updatedStudent)
        {
            var existingStudent = StudentData.Students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;
            existingStudent.Grade = updatedStudent.Grade;

            return NoContent();
        }

        [HttpGet("total")]
        public ActionResult<int> GetTotalStudentsCount()
        {
            return StudentData.Students.Count;
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = StudentData.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            StudentData.Students.Remove(student);
            return NoContent();
        }
    }
}
