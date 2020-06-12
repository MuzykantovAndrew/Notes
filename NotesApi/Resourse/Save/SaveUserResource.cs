namespace NotesApi.Resourse
{
    public class SaveUserResource : IResourse
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Info { get; set; }
    }
}