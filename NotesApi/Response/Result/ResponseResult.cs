using NotesApi.Resourse;

namespace NotesApi.Response.Result
{
    public class ResponseResult
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}