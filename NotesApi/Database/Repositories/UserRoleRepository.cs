using NotesApi.Database.Context;
using NotesApi.Models;

namespace NotesApi.Database.Repositories
{
    public class UserRoleRepository : BaseRepository<UserRole>
    {
             public UserRoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}