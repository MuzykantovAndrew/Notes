using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Extentions;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Services;

namespace NotesApi.Controllers
{
    [Route("/api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService authService;
        private readonly IMapper mapper;
        public AuthController(IAuthenticationService authService,
                              IMapper mapper)
        {
            this.authService = authService;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User user)
        {
            var authenticatedUser = await authService.AuthenticateAsync(user.Login, user.Password);
            var userResource = mapper.Map<User, UserResourse>(authenticatedUser.User);
            var result = authenticatedUser.GetResponseResult(userResource);
            return Ok(result);
        }
    }
}