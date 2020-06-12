using NotesApi.Database.Context;
using NotesApi.Models;

namespace NotesApi.Database.Repositories
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}