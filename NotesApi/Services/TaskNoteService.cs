using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NotesApi.Database.Infrastructer;
using NotesApi.Database.Interfaces;
using NotesApi.Models;
using NotesApi.Response;
using NotesApi.Services.Interfaces;

namespace NotesApi.Services
{
    public class TaskNoteService : ITaskNoteService
    {
        private readonly IRepository<TaskNote> taskNoteRepository;
        private readonly IUnitOfWork unitOfWork;
        public TaskNoteService(IRepository<TaskNote> categoryRepository, IUnitOfWork unitOfWork)
        {
            this.taskNoteRepository = categoryRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TaskNoteResponse> DeleteAsync(int id)
        {
            var existingtaskNote = await taskNoteRepository.FindByIdAsync(id);
            if (existingtaskNote == null)
                return new TaskNoteResponse("taskNote not found");

            try
            {
                taskNoteRepository.Remove(existingtaskNote);
                await unitOfWork.CompleteAsync();

                return new TaskNoteResponse(existingtaskNote);
            }
            catch (Exception ex)
            {
                return new TaskNoteResponse($"Error when deleting taskNote: {ex.Message}");
            }

        }

        public async Task<IEnumerable<TaskNote>> ListAsync()
        {
            return await taskNoteRepository.ListAsync();
        }

        public async Task<TaskNoteResponse> SaveAsync(TaskNote taskNote)
        {
            try 
            {
                await taskNoteRepository.AddAsync(taskNote);
                await unitOfWork.CompleteAsync();

                return new TaskNoteResponse(taskNote);
            }
            catch (Exception ex)
            {
                return new TaskNoteResponse($"Error occured when saving taskNote: {ex.Message}");
            }
        }

        public async Task<TaskNoteResponse> UpdateAsync(int id, TaskNote taskNote)
        {
            var existingtaskNote  = await taskNoteRepository.FindByIdAsync(id);

            if (existingtaskNote == null)
                return new TaskNoteResponse("taskNote not found");

            existingtaskNote.Header = taskNote.Header;
            existingtaskNote.Description = taskNote.Description;
            existingtaskNote.Priority = taskNote.Priority;
            existingtaskNote.CreationTime = taskNote.CreationTime;
            existingtaskNote.Complete = taskNote.Complete;


            try
            {
                taskNoteRepository.Update(existingtaskNote);
                await unitOfWork.CompleteAsync();
                return new TaskNoteResponse(existingtaskNote);
            }
            catch (Exception ex)
            {
                return new TaskNoteResponse($"taskNote update error: {ex.Message}");
            }
        }
    }
}
