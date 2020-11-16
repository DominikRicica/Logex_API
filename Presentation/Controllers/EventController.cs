using AutoMapper;
using Business.Filters;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Presentation.Helpers;
using Presentation.Models;
using Presentation.Queries;
using Presentation.Routes;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Event.Get)]
        public IActionResult Get(int Id, [FromQuery] GetDetailQuery query)
        {
            var filter = _mapper.Map<GetDetailFilter>(query);
            var eventBO = _eventService.GetEventDetail(Id, filter);
            var eventResponse = _mapper.Map<Item>(eventBO);
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

            var eventsBOs = _eventService.GetEvents(filter, pagination);
            var events = _mapper.Map<List<ListItem>>(eventsBOs);

            var totalEvents = _eventService.GetEventTotalCount(filter);

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(pagination, totalEvents);
            Response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationResponse));

            return Ok(events);
        }
    }
}
