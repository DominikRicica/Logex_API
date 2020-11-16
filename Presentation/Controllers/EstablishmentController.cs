using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Filters;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Helpers;
using Presentation.Models;
using Presentation.Queries;
using Presentation.Routes;
namespace Presentation.Controllers
{
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IMapper _mapper;

        public EstablishmentController(IEstablishmentService establishmentService,IMapper mapper)
        {
            _establishmentService = establishmentService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Establishment.Get)]
        public IActionResult Get(int Id, [FromQuery] GetDetailQuery query)
        {
            var filter = _mapper.Map<GetDetailFilter>(query);
            var establishmentBO = _establishmentService.GetEstablishmentDetail(Id, filter);
            var establishment = _mapper.Map<Item>(establishmentBO);
            if(establishment == null)
            {
                return NotFound();
            }
            return Ok(establishment);
        }
        [HttpGet(ApiRoutes.Establishment.GetAll)]
        public IActionResult GetAll([FromQuery] GetListQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var filter = _mapper.Map<GetListFilter>(query);
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);

            var establishmentsBOs = _establishmentService.GetEstablishments(filter, pagination);
            var establishments = _mapper.Map<List<ListItem>>(establishmentsBOs);

            var totalEstablishments = _establishmentService.GetEstablishmentsTotalCount(filter);

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(pagination, totalEstablishments);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationResponse));

            return Ok(establishments);
        }
    }
}
