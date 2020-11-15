using AutoMapper;
using Business.BusinessObjects;
using Business.Filters;
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
        private readonly IMapper _mapper;

        private const int EarthRadius = 6371;
        public EstablishmentService(IEstablishmentRepository establishmentRepository, IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _establishmentRepository = establishmentRepository;
            _mapper = mapper;
        }
        public ItemBO GetEstablishmentDetail(int id, GetDetailFilter filter)
        {
            var dbEstablishmentDetail = _establishmentRepository.GetEstablishmentDetail(id, filter.LanguageCode);
            var establishmentDetail = _mapper.Map<ItemBO>(dbEstablishmentDetail);
            return establishmentDetail;
        }
        public List<ListItemBO> GetEstablishments(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEstablishments = _establishmentRepository.GetEstablishments(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var establishments = _mapper.Map<List<ListItemBO>>(dbEstablishments);
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
            var dbEvents = _eventRepository.GetEventsInRadius(Decimal.Parse(dbEstablishment.Latitude), Decimal.Parse(dbEstablishment.Longitude), 1, EarthRadius);
            var events = _mapper.Map<List<ListItemBO>>(dbEvents);
            return events;
        }
    }
}
