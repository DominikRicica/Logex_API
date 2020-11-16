using AutoMapper;
using Business.BusinessObjects;
using Business.Filters;
using Business.Helpers;
using DataAccess.Entities;
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
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        private const int MaxDistance = 1000;
        public EventService(IEventRepository eventRepository, IEstablishmentRepository establishmentRepository, ILocationService locationService, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _locationService = locationService;
            _establishmentRepository = establishmentRepository;
            _mapper = mapper;
        }
        public ItemBO GetEventDetail(int id, GetDetailFilter filter)
        {
            var dbEventDetail = _eventRepository.GetEventDetail(id, filter.LanguageCode);
            var eventDetail = _mapper.Map<ItemBO>(dbEventDetail);
            if (eventDetail == null)
            {
                return null;
            }
            StringHelper.ClearDescriptions(eventDetail.Descriptions);
            StringHelper.RemoveYoutubeLink(eventDetail.ImageLinks);
            return eventDetail;
        }
        public List<ListItemBO> GetEvents(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEvents = _eventRepository.GetEvents(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var events = _mapper.Map<List<ListItemBO>>(dbEvents);
            foreach (var e in events)
            {
                StringHelper.ClearDescriptions(e.Descriptions);
            }
            return events;
        }
        public int GetEventTotalCount(GetListFilter filter)
        {
            var totalCount = _eventRepository.GetEventCount(filter.Name, filter.City);
            return totalCount;
        }
        public List<ListItemBO> GetEstablishmentInRadius(int eventId, int radius = 1)
        {
            var dbEvent = _eventRepository.GetEventDetail(eventId);
            if (dbEvent == null)
            {
                return null;
            }
            var dbEstablishments = new List<ItemEntity>(_establishmentRepository.GetAllEstablishments());

            for (int i = dbEstablishments.Count - 1; i >= 0; i--)
            {
                var distance = _locationService.GetDistance(Double.Parse(dbEvent.Longitude), Double.Parse(dbEvent.Latitude),
                    Double.Parse(dbEstablishments[i].Longitude), Double.Parse(dbEstablishments[i].Latitude));
                if (distance > MaxDistance)
                {
                    dbEstablishments.RemoveAt(i);
                }
            }

            var establishments = _mapper.Map<List<ListItemBO>>(dbEstablishments);
            return establishments;
        }
    }
}
