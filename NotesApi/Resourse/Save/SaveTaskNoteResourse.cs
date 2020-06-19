using System;


namespace NotesApi.Resourse.Save
{
    public class SaveTaskNoteResourse
    {
         public int UserId { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime CreationTime { get; set; }
       
        public bool Complete { get; set; }
    }
}