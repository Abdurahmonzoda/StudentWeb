using Services.Services.cs;
using Services.Services;
using Domail.Response;
using Domail.Emtities;
using Microsoft.AspNetCore.Mvc;

namespace Students.Controllers
{
    public class StudentController
    {
        private StudentServices _studentServices;
        public StudentController(StudentServices studentServices)
        {
           _studentServices = studentServices;
        }

        [HttpPost("AddStudent")]
        public async Task<Response<Student>> AddStudent(Student student)
        {
            return await _studentServices.AddStudent(student); 
        }


        [HttpGet("GetAllStudents")]
        public async Task<Response<List<Student>>> GetStudents()
        {
            return await _studentServices.GetStudents();
        }


        [HttpPut("UpdateStudent")]
        public async Task<Response<Student>> UpdateStudent(Student student)
        {
            return await _studentServices.UpdateStudent(student);
        }

        [HttpDelete("DeleteStudent")]
        public async Task<Response<string>> UpdateStudent(int id)
        {
            return await _studentServices.DeleteStudent(id);
        }


    }
}
