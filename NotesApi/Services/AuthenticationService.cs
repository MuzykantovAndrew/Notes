using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NotesApi.Database.Interfaces;
using NotesApi.Extentions;
using NotesApi.Helpers;
using NotesApi.Models;
using NotesApi.Response;

namespace NotesApi.Services
{
     public class AuthenticationService : IAuthenticationService
    {
        private readonly AppSettings appSettings;
        private readonly IRepository<User> userRepository;
        public AuthenticationService(IOptions<AppSettings> appSettings,
                           IRepository<User> userRepository)
        {
            this.appSettings = appSettings.Value;
            this.userRepository = userRepository;
        }
        public async Task<UserResponse> AuthenticateAsync(string login, string password)
        {
            User user = (await userRepository.ListAsync())
                            .SingleOrDefault(usr => usr.Login == login && usr.Password == password);

            if (user == null)
                return new UserResponse("Invalid login or password");

            try
            {         
                user.GenerateTokenString(appSettings.Secret, appSettings.TokenExpires);
                user.Password = null;
                return new UserResponse(user);

            }
            catch (Exception ex)
            {
                 return new UserResponse($"An error occured when authenticating user: {ex.Message}");
            }


        }
    }
}