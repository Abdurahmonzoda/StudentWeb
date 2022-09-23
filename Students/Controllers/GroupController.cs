using Domail.Emtities;
using Domail.Response;
using Microsoft.AspNetCore.Mvc;
using Services.Services.cs;

namespace Students.Controllers
{
    public class GroupController
    {
        private GroupServices _studentServices;

        public GroupController(GroupServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpPost("AddGroup")]
        public async Task<Response<Group>> AddGroup(Group group)
        {
            return await _studentServices.AddGroup(group);
        }


        [HttpGet("GetAllGroup")]
        public async Task<Response<List<Group>>> GetGroup()
        {
            return await _studentServices.GetGroup();
        }


        [HttpPut("UpdateGroup")]
        public async Task<Response<Group>> UpdateMentor(Group group)
        {
            return await _studentServices.UpdateGroup(group);
        }

        [HttpDelete("DeleteGroup")]
        public async Task<Response<string>> UpdateGroup(int id)
        {
            return await _studentServices.DeleteGroup(id);
        }

    }
}
