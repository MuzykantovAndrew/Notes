using System;

namespace NotesApi.Models
{
    public class TaskNote
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
        public bool Complete { get; set; }
    }
}