using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Extentions;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Resourse.Save;
using NotesApi.Response.Result;
using NotesApi.Services.Interfaces;

namespace NotesApi.Controllers
{
    public class UserRoleController: Controller
    {
        private readonly IUserRoleService userRoleService;
        private readonly IMapper mapper;
        public UserRoleController(IUserRoleService userRoleService, IMapper mapper)
        {
            this.userRoleService = userRoleService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> ListAsync(int id)
        {
            var users = await userRoleService.ListUsersByRoleAsync(id);
            var Resourses = mapper.Map<IEnumerable<User>, IEnumerable<UserResourse>>(users);
          
            var result = new ResponseResult 
            {
                Success = true,
                Message = "",
                Data = Resourses
            };
            return result;
        }



        [HttpPost]
        [Route("setrole")]
        public async Task<IActionResult> SetRole([FromBody]SaveUserRoleResourse Resourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var userResponse = await userRoleService.SetUserRoleAsync(Resourse.UserId, Resourse.RoleId);
            var userResourse = mapper.Map<User, UserResourse>(userResponse.User);
            var result = userResponse.GetResponseResult(userResourse);
            return Ok(result);

        }

        [HttpDelete]
        [Route("deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(int id, [FromBody] SaveUserRoleResourse Resourse)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var userResponse = await userRoleService.DeleteRoleAsync(id, Resourse.RoleId);
            var userResourse = mapper.Map<User, UserResourse>(userResponse.User);
            var result = userResponse.GetResponseResult(userResourse);
            return Ok(result);
        }
    }
   
}