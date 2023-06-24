using DWShop.Application.Responses.Identity;
using DWShop.Domain.Entities;
using AutoMapper;
using DWShop.Application.Features.Identitty.Commands.Register;

namespace DWShop.Application.Mappings
{
    /// <summary>
    /// Esta clase la usaremos para trasformar los objetos a traves del mapping (AutoMapper)
    /// del DWUser a un LoginResult cuando son diferentes.
    /// </summary>
    public class IdentityProfile :Profile
    {
        public IdentityProfile()
        {
            CreateMap<DWUser,LoginResponse>()
                .ForMember(dest => dest.UserName, user =>user.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Gafette, user => user.MapFrom(src => src.Gafette));

            CreateMap<RegisterUserCommand, DWUser>().ReverseMap();
        }
    }
}
