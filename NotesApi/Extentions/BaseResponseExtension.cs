using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Response;
using NotesApi.Response.Result;

namespace NotesApi.Extentions
{
    public static class BaseResponseExtension
    {
        public static ResponseResult GetResponseResult<T>(this T response, IResourse resource)where T: BaseResponse 
        {
            return new ResponseResult
            {
                Data = resource,
                Message = response.Message,
                Success = response.Success
            };
        }
    }
}