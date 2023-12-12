using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkshopSolution.DataTransferObjects;
using WorkshopSolution.DomainModels;

namespace WorkshopSolution.BusinessLogic.Mappings
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<Rack, RackDetailDto>().ReverseMap();

      CreateMap<Rack, RackListDto>();
    }
  }
}
