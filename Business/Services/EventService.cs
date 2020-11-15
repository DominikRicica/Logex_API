using AutoMapper;
using Business.BusinessObjects;
using Business.Filters;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
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
        public ItemBO GetEventDetail(int id, GetDetailFilter filter)
        {
            var dbEventDetail = _eventRepository.GetEventDetail(id, filter.LanguageCode);
            var eventDetail = _mapper.Map<ItemBO>(dbEventDetail);
            return eventDetail;
        }
        public List<ListItemBO> GetEvents(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEvents = _eventRepository.GetEvents(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var events = _mapper.Map<List<ListItemBO>>(dbEvents);
            return events;
        }
        public int GetEventTotalCount(GetListFilter filter)
        {
            var totalCount = _eventRepository.GetEventCount(filter.Name, filter.City);
            return totalCount;
        }
        public List<ListItemBO> GetEstablishmentInRadius(int eventId, int radius)
        {
            var dbEvent = _eventRepository.GetEventDetail(eventId);
            var dbEstablishments = _establishmentRepository.GetEstablishmentsInRadius(Decimal.Parse(dbEvent.Latitude), Decimal.Parse(dbEvent.Longitude), radius, EarthRadius);
            var establishments = _mapper.Map<List<ListItemBO>>(dbEstablishments);
            return establishments;
        }
    }
}
