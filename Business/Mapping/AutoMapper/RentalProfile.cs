using AutoMapper;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Mapping.AutoMapper
{
    public class RentalProfile:Profile
    {
        public RentalProfile()
        {
            CreateMap<Rental, RentalDetailDto>()
                .ForMember(dest=>dest.RentDate,opt=>opt.MapFrom(x=>x.RentDate))
                .ForMember(dest=>dest.ReturnDate,opt=>opt.MapFrom(x=>x.ReturnDate));
        }
    }
}
