using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping.AutoMapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserForLoginDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email));

            CreateMap<User, UserForRegisterDto>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Email));
        }
    }
}
