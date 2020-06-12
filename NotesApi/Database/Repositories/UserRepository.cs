using System.Buffers.Text;
using NotesApi.Database.Context;
using NotesApi.Models;

namespace NotesApi.Database.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
             public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}