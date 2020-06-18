using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Extentions;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Response;
using NotesApi.Response.Result;
using NotesApi.Services;
using NotesApi.Services.Interfaces;

namespace NotesApi.Controllers
{
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        const int ITEMS_PER_PAGE = 10;
        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ResponseResult> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var count = (int)Math.Ceiling((decimal)users.Count() / 10);
            var result = new ResponseResult
            {
                Data = count,
                Message = "",
                Success = true
            };
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ResponseResult> GetAllAsync(int id)
        {
            var users = await userService.ListAsync();
            var collection = users.Skip((id - 1) * ITEMS_PER_PAGE)
                                                     .Take(ITEMS_PER_PAGE);
            var pageCount = (int)Math.Ceiling((decimal)users.Count() / ITEMS_PER_PAGE);
            if (id > pageCount)
            {
                var result = new ResponseResult
                {
                    Data = null,
                    Message = "Page not Found",
                    Success = false
                };
                return result;
            }
            else
            {
                var resources = mapper.Map<IEnumerable<User>, IEnumerable<UserResourse>>(collection);
                var result = new ResponseResult
                {
                    Data = resources,
                    Message = "",
                    Success = true
                };
                return result;
            }




        }


        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var us = await userService.ListAsync();
            var i = us.Where(x => x.Login == resource.Login);
            if (i == null)
                return BadRequest(ModelState.GetErrorMessages());

            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.SaveAsync(user);
            User ur = userResponse.User;
            var userResourse = mapper.Map<User, UserResourse>(ur);
            var result = userResponse.GetResponseResult(userResourse);
            return Ok(result);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var us = await userService.ListAsync();
            var i = us.Where(x => x.Login == resource.Login);
            if (i == null)
                return BadRequest(ModelState.GetErrorMessages());
            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.UpdateAsync(id, user);
            var userResource = mapper.Map<User, UserResourse>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userResponse = await userService.DeleteAsync(id);
            var userResource = mapper.Map<User, UserResourse>(userResponse.User);
            var result = userResponse.GetResponseResult(userResource);
            return Ok(result);
        }
    }
}
