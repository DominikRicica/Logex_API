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
    public class EstablishmentService : IEstablishmentService
    {
        private readonly IEstablishmentRepository _establishmentRepository;
        private readonly IEventRepository _eventRepository;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;

        private const int MaxDistance = 1000;
        public EstablishmentService(IEstablishmentRepository establishmentRepository, IEventRepository eventRepository, ILocationService locationService, IMapper mapper)
        {
            _locationService = locationService;
            _eventRepository = eventRepository;
            _establishmentRepository = establishmentRepository;
            _mapper = mapper;
        }
        public ItemBO GetEstablishmentDetail(int id, GetDetailFilter filter)
        {
            var dbEstablishmentDetail = _establishmentRepository.GetEstablishmentDetail(id, filter.LanguageCode);
            var establishmentDetail = _mapper.Map<ItemBO>(dbEstablishmentDetail);
            if (establishmentDetail == null)
            {
                return null;
            }
            StringHelper.ClearDescriptions(establishmentDetail.Descriptions);
            StringHelper.RemoveYoutubeLink(establishmentDetail.ImageLinks);
            return establishmentDetail;
        }
        public List<ListItemBO> GetEstablishments(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEstablishments = _establishmentRepository.GetEstablishments(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var establishments = _mapper.Map<List<ListItemBO>>(dbEstablishments);
            foreach (var establishment in establishments)
            {
                StringHelper.ClearDescriptions(establishment.Descriptions);
            }
            return establishments;
        }
        public int GetEstablishmentsTotalCount(GetListFilter filter)
        {
            var totalCount = _establishmentRepository.GetEstablishmentCount(filter.Name, filter.City);
            return totalCount;
        }

        public List<ListItemBO> GetEventsInRadius(int establishmentId)
        {
            var dbEstablishment = _establishmentRepository.GetEstablishmentDetail(establishmentId);
            if (dbEstablishment == null)
            {
                return null;
            }
            var dbEvents = new List<ItemEntity>(_eventRepository.GetAllEvents());

            for (int i = dbEvents.Count - 1; i >= 0; i--)
            {
                var test = dbEvents[i];
                var distance = _locationService.GetDistance(Double.Parse(dbEstablishment.Longitude), Double.Parse(dbEstablishment.Latitude),
                    Double.Parse(dbEvents[i].Longitude), Double.Parse(dbEvents[i].Latitude));
                if (distance > MaxDistance)
                {
                    dbEvents.RemoveAt(i);
                }
            }

            var events = _mapper.Map<List<ListItemBO>>(dbEvents);
            return events;
        }
    }
}
