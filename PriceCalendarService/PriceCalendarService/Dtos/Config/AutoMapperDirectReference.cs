using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceCalendarService.Dtos.Config
{
    public class AutoMapperDirectReference
    {
        private IMapper _mapper { get; }
        private  AutoMapperDirectReference(IMapper mapper){ _mapper = mapper; }
        
    }
}
