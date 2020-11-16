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
    public class LocationAwareEstablishment : ControllerBase
    {
        private readonly IEstablishmentService _establishmentService;
        private readonly IMapper _mapper;

        public LocationAwareEstablishment(IEstablishmentService establishmentService, IMapper mapper)
        {
            _establishmentService = establishmentService;
            _mapper = mapper;
        }
        [HttpGet(ApiRoutes.LocationAwarenessEstablishment.GetNearEvents)]
        public IActionResult GetNearEvents(int establishmentId)
        {
            var eventsBOs = _establishmentService.GetEventsInRadius(establishmentId);
            var events = _mapper.Map<List<ListItem>>(eventsBOs);

            if (events.Count == 0)
            {
                return NotFound();
            }

            return Ok(events);
        }
    }
}
