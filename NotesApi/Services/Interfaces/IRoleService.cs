using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Models;
using NotesApi.Response;

namespace NotesApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task<RoleResponse> DeleteAsync(int id);
        Task<IEnumerable<Role>> ListAsync();
        Task<RoleResponse> SaveAsync(Role role);
        Task<RoleResponse> UpdateAsync(int id, Role role);
    }
}