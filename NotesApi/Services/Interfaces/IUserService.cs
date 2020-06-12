using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Models;
using NotesApi.Response;

namespace NotesApi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> DeleteAsync(int id);
        Task<IEnumerable<User>> ListAsync();
        Task<UserResponse> SaveAsync(User user);
        Task<UserResponse> UpdateAsync(int id, User user);
        
    }
}