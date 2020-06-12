using NotesApi.Models;

namespace NotesApi.Response
{
    public class TaskNoteResponse : BaseResponse
    {
        public TaskNote TaskNote {get; private set;}

        public TaskNoteResponse(bool success, string message, TaskNote TaskNote) : base(success, message)
        {
            this.TaskNote = TaskNote;
        }

        public TaskNoteResponse(TaskNote TaskNote) : this(true, string.Empty, TaskNote) {}
        
        
        public TaskNoteResponse(string message) : this(false, message, null) {}
    }
   
}