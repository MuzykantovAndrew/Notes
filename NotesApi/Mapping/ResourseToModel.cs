using System.Linq;
using AutoMapper;
using NotesApi.Models;
using NotesApi.Resourse;
using NotesApi.Resourse.Save;

namespace NotesApi.Mapping
{
    public class ResourseToModel : Profile
    {
        public ResourseToModel()
        {
            CreateMap<UserResourse, User>();
              CreateMap<SaveUserResource, User>();
               CreateMap<SaveRoleResourse, Role>();
               CreateMap<RoleResourse, Role>();

               CreateMap<SaveTaskNoteResourse, TaskNote>();
               CreateMap<TaskNoteResourse,TaskNote>();
               
        }
    }
}
