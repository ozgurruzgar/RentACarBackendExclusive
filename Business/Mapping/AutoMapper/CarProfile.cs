using AutoMapper;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping.AutoMapper
{
    public class CarProfile:Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDetailDto>()
            .ForMember(dest => dest.CarId, opt => opt.MapFrom(x => x.CarId))
            .ForMember(dest => dest.BrandId, opt => opt.MapFrom(x => x.BrandId))
            .ForMember(dest => dest.ColorId, opt => opt.MapFrom(x => x.ColorId))
            .ForMember(dest => dest.ModelYear, opt => opt.MapFrom(x => x.ModelYear))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(x => x.Description))
            .ForMember(dest => dest.DailyPrice, opt => opt.MapFrom(x => x.DailyPrice));

        }
    }
}
