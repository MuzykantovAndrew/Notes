using System.Threading.Tasks;
using NotesApi.Response;

namespace NotesApi.Services
{
     public interface IAuthenticationService
    {
        Task<UserResponse> AuthenticateAsync(string login, string password);
         
    }
}