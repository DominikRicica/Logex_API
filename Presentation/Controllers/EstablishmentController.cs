using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Filters;
using Presentation.Helpers;
using Presentation.Models;
using Presentation.Queries;
using Presentation.Routes;
using Presentation.Services;

namespace Presentation.Controllers
{
    [ApiController]
    public class EstablishmentController : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EstablishmentController(IEstablishmentService establishmentService,IMapper mapper, IUriService uriService)
        {
            _establishmentService = establishmentService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Establishment.Get)]
        public IActionResult Get(int Id, [FromQuery] GetDetailQuery query)
        {
            var filter = _mapper.Map<GetDetailFilter>(query);
            var establishmentResponse = _establishmentService.GetEstablishmentDetail(Id, filter);
            if(establishmentResponse == null)
            {
                return NotFound();
            }
            return Ok(establishmentResponse);
        }
        [HttpGet(ApiRoutes.Establishment.GetAll)]
        public IActionResult GetAll([FromQuery] GetListQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var filter = _mapper.Map<GetListFilter>(query);
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var establishmentsResponse = _establishmentService.GetEstablishments(filter, pagination);
            var totalEstablishments = _establishmentService.GetEstablishmentsTotalCount(filter);

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(pagination, totalEstablishments);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationResponse));
            return Ok(establishmentsResponse);
        }
    }
}
