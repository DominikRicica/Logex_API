using AutoMapper;
using Presentation.Filters;
using Presentation.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Mapping
{
    public class RequestToFilterProfile : Profile
    {
        public RequestToFilterProfile()
        {
            CreateMap<GetDetailQuery, GetDetailFilter>();
            CreateMap<GetListQuery, GetListFilter>();
            CreateMap<PaginationQuery, PaginationFilter>();
        }      
    }
}
