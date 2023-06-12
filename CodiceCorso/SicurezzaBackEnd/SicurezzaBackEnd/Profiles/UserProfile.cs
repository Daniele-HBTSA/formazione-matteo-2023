using AutoMapper;
using GestUser.Dtos;
using Models;

namespace GestUser.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Utenti, UtentiDto>();
            CreateMap<Profili, ProfiliDto>();
            
            /*
            .ForMember
            (
                dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId.Trim())
            )
            .ForMember
            (
                dest => dest.CodFidelity,
                opt => opt.MapFrom(src => src.CodFidelity.Trim())
            )
            .ForMember
            (
                dest => dest.Password,
                opt => opt.MapFrom(src => src.Password.Trim())
            )
            .ForMember
            (
                dest => dest.Abilitato,
                opt => opt.MapFrom(src => src.Abilitato.Trim())
            );
            */
        }
    
    }
}