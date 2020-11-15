using AutoMapper;
using DataAccess;
using DataAccess.Repository;
using Presentation.Filters;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly IMapper _mapper;

        private const int EarthRadius = 6371;
        public EventService(IEventRepository eventRepository, IEstablishmentRepository establishmentRepository, IMapper mapper)
        {            
            _eventRepository = eventRepository;
            _establishmentRepository = establishmentRepository;
            _mapper = mapper;
        }
        public Item GetEventDetail(int id, GetDetailFilter filter)
        {
            var dbEventDetail = _eventRepository.GetEventDetail(id, filter.LanguageCode);
            var eventDetail = _mapper.Map<Item>(dbEventDetail);
            return eventDetail;
        }
        public List<ListItem> GetEvents(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEvents = _eventRepository.GetEvents(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var events = _mapper.Map<List<ListItem>>(dbEvents);
            return events;
        }
        public int GetEventTotalCount(GetListFilter filter)
        {
            var totalCount = _eventRepository.GetEventCount(filter.Name, filter.City);
            return totalCount;
        }
        public List<ListItem> GetEstablishmentInRadius(int eventId, int radius)
        {
            var dbEvent = _eventRepository.GetEventDetail(eventId);
            var dbEstablishments = _establishmentRepository.GetEstablishmentsInRadius(Decimal.Parse(dbEvent.Latitude), Decimal.Parse(dbEvent.Longitude), radius, EarthRadius);
            var establishments = _mapper.Map<List<ListItem>>(dbEstablishments);
            return establishments;
        }
    }
}
