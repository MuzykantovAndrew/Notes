using System.Linq;
using AutoMapper;
using NotesApi.Models;
using NotesApi.Resourse;

namespace NotesApi.Mapping
{
    public class ModelToResourse: Profile
    {
        public ModelToResourse()
        {
            CreateMap<User, UserResourse>()
                .ForMember(x => x.Role, y => y.MapFrom(s => s.UserRoles.Select(z => z.Role.Name)));
            


        }
    }
}