using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Models;
using NotesApi.Response;

namespace NotesApi.Services.Interfaces
{
    public interface ITaskNoteService
    {
        Task<TaskNoteResponse> DeleteAsync(int id);
        Task<IEnumerable<TaskNote>> ListAsync();
        Task<TaskNoteResponse> SaveAsync(TaskNote taskNote);
        Task<TaskNoteResponse> UpdateAsync(int id, TaskNote taskNote);
    }
}