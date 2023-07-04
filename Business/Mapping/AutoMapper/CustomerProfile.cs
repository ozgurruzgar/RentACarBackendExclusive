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
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDetailDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(x => x.CompanyName));

        }
    }
}
