using Domail.Emtities;
using Domail.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Services.cs;

namespace Students.Controllers
{
    public class MentorController
    {
        private MentorServices _studentServices;

        public MentorController(MentorServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpPost("AddMentor")]
        public async Task<Response<Mentor>> AddMentor(Mentor student)
        {
            return await _studentServices.AddMentor(student);
        }


        [HttpGet("GetAllMentors")]
        public async Task<Response<List<Mentor>>> GetMentors()
        {
            return await _studentServices.GetMentors();
        }


        [HttpPut("UpdateMentor")]
        public async Task<Response<Mentor>> UpdateMentor(Mentor mentor)
        {
            return await _studentServices.UpdateMentor(mentor);
        }

        [HttpDelete("DeleteMentor")]
        public async Task<Response<string>> UpdateMentor(int id)
        {
            return await _studentServices.DeleteMentor(id);
        }

    }
}
