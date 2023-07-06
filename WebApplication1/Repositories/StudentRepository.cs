using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DataContext;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class StudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public bool IsEmailUnique(string email)
        {
            return !_context.Students.Any(s => s.Email == email);
        }


        public List<Student> GetAllStudents()
        {
            return _context.Students.ToList();
        }

        public void DeleteStudentByEmail(string email)
        {
            var student = _context.Students.FirstOrDefault(s => s.Email == email);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }
    }
}
