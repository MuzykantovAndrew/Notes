namespace NotesApi.Resourse
{
    public class UserResourse: IResourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Login { get; set; }
        public string Info { get; set; }
        public string Token { get; set; }
        public string[] Role { get; set; }
    }
}