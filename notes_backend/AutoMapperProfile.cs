using AutoMapper;
using notes_backend.Entities.DataTransferObjects;
using notes_backend.Entities.Models;

namespace notes_backend
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDTO, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
            CreateMap<NoteDTO, Note>()
                .ForMember(n => n.UserId, opt => opt.MapFrom(x => x.UserId));
        }
        
    }
}
