using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApi.Models
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<TaskNote> TaskNotes { get; set; } = new List<TaskNote>();
        public string Password { get; set; }
        public string Info { get; set; }
        [NotMapped]
        public string Token { get; set; }

    }
}