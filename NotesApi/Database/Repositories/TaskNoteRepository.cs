using System.Threading.Tasks;
using NotesApi.Database.Context;
using NotesApi.Models;

namespace NotesApi.Database.Repositories
{
    public class TaskNoteRepository : BaseRepository<TaskNote>
    {
        public TaskNoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}