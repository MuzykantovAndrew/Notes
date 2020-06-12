using System.Linq;
using AutoMapper;
using NotesApi.Models;
using NotesApi.Resourse;

namespace NotesApi.Mapping
{
    public class ResourseToModel : Profile
    {
        public ResourseToModel()
        {
            CreateMap<UserResourse, User>();
              CreateMap<SaveUserResource, User>();
        }
    }
}
