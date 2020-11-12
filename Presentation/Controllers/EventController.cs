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
using Presentation.Queries;
using Presentation.Routes;
using Presentation.Services;

namespace Presentation.Controllers
{
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EventController(IItemService itemService, IMapper mapper, IUriService uriService)
        {
            _itemService = itemService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.Event.Get)]
        public IActionResult Get(int Id, [FromQuery] GetDetailQuery query)
        {
            var filter = _mapper.Map<GetDetailFilter>(query);
            var eventResponse = _itemService.GetEventDetail(Id, filter);
            if (eventResponse == null)
            {
                return NotFound();
            }
            return Ok(eventResponse);
        }
        [HttpGet(ApiRoutes.Event.GetAll)]
        public IActionResult GetAll([FromQuery] GetListQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var filter = _mapper.Map<GetListFilter>(query);
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var eventsResponse = _itemService.GetEvents(filter, pagination);
            var totalEvents = _itemService.GetEventTotalCount(filter);

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, eventsResponse, totalEvents);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationResponse));
            return Ok(eventsResponse);
        }
    }
}
