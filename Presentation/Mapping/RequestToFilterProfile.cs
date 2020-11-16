using AutoMapper;
using Business.BusinessObjects;
using Business.Filters;
using DataAccess.Entities;
using Presentation.Models;
using Presentation.Queries;
using System.Linq;

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
