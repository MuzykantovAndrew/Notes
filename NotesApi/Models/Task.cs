using System;
using NotesApi.Models.Enum;

namespace NotesApi.Models
{
    public class Task
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public PriorityTypes? Priority { get; set; }
        public DateTime CreationTime { get; set; }
        public User User { get; set; }
    }
}