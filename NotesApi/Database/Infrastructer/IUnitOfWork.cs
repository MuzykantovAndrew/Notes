using System.Threading.Tasks;

namespace NotesApi.Database.Infrastructer
{
      public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}