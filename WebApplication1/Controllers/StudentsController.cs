using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(StudentRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public IActionResult EnrollStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.IsEmailUnique(student.Email))
            {
                return BadRequest("Email already exists.");
            }

            // Additional validation and processing logic can be added here

            _repository.Add(student);
            return Ok("Student is Registered");
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _repository.GetAllStudents();
            return Ok(students);
        }

        [HttpDelete("{email}")]
        public IActionResult DeleteStudent(string email)
        {
            if (_repository.IsEmailUnique(email))
            {
                return BadRequest("Email not exists.");
            }

            _repository.DeleteStudentByEmail(email);
            return Ok("Deleted");
        }
    }
}
