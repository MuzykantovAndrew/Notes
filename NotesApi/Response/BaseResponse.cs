using System.Threading.Tasks;
using NotesApi.Models;
using NotesApi.Response.Result;

namespace NotesApi.Response
{
    public abstract class BaseResponse
    {
        public bool Success {get; protected set;}
        public string Message {get; protected set;}

        public BaseResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}