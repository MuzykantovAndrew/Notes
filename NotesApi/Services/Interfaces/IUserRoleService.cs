using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Models;
using NotesApi.Response;

namespace NotesApi.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserResponse> DeleteRoleAsync(int userId, int roleId);
        Task<IEnumerable<User>> ListUsersByRoleAsync(int roleId);
        Task<UserResponse> SetUserRoleAsync(int userId, int roleId);
    }
}