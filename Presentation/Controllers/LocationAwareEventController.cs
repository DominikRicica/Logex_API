using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using Presentation.Routes;

namespace Presentation.Controllers
{
    [ApiController]
    public class LocationAwareEventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public LocationAwareEventController(IEventService eventService, IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }
        [HttpGet(ApiRoutes.LocationAwarenessEvent.GetNearEstablishments)]
        public IActionResult GetNearEstablishments(int eventId)
        {
            var establishmentsBOs = _eventService.GetEstablishmentInRadius(eventId);
            var establishments = _mapper.Map<List<ListItem>>(establishmentsBOs);
            if (establishments.Count == 0)
            {
                return NotFound();
            }
            return Ok(establishments);
        }
    }
}
