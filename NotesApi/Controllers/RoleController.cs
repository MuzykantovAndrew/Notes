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
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IMapper mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            this.roleService = roleService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseResult> GetAllAsync()
        {
            var roles = await roleService.ListAsync();
            var resources = mapper.Map<IEnumerable<Role>, IEnumerable<RoleResourse>>(roles);
            var result = new ResponseResult
            {
                Data = resources,
                Message = "",
                Success = true
            };
            return result;
        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoleResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResourse, Role>(resource);
            var roleResponse = await roleService.SaveAsync(role);

            var roleResource = mapper.Map<Role, RoleResourse>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);

        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoleResourse resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var role = mapper.Map<SaveRoleResourse, Role>(resource);
            var roleResponse = await roleService.UpdateAsync(id, role);
            var roleResource = mapper.Map<Role, RoleResourse>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);
        }

         
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id) 
        {
            var roleResponse = await roleService.DeleteAsync(id);
            var roleResource = mapper.Map<Role, RoleResourse>(roleResponse.Role);
            var result = roleResponse.GetResponseResult(roleResource);
            return Ok(result);
        }
    }
}