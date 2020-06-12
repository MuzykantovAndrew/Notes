using System;
using NotesApi.Models.Enum;

namespace NotesApi.Resourse.Save
{
    public class SaveTaskNoteResourse
    {
         public int UserId { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
        public PriorityTypes? Priority { get; set; }
        public DateTime CreationTime { get; set; }
       
        public bool Complete { get; set; }
    }
}