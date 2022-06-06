using AutoMapper;
using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>() //especificamos el objeto de origen y el objeto de destino para mapea
            .ForMember(c => c.FullAddress,
            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country))); //reglas de mapeo adicionales

            CreateMap<Employee, EmployeeDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<EmployeeForCreationDto, Employee>();
            CreateMap<EmployeeForUpdateDto, Employee>();

        }


    }
}
