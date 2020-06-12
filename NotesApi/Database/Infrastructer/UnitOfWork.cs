using System.Threading.Tasks;
using NotesApi.Database.Context;

namespace NotesApi.Database.Infrastructer
{
     public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}