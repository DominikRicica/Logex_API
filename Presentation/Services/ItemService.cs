using AutoMapper;
using DataAccess;
using DataAccess.Entities;
using Presentation.Filters;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }
        public ItemResponse GetEstablishmentDetail(int id, GetDetailFilter filter)
        {
            var dbEstablishmentDetail = _itemRepository.GetEstablishmentDetail(id, filter.LanguageCode);
            var establishmentDetail = _mapper.Map<ItemResponse>(dbEstablishmentDetail);
            return establishmentDetail;
        }

        public List<ListItemResponse> GetEstablishments(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEstablishments = _itemRepository.GetEstablishments(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var establishments = _mapper.Map<List<ListItemResponse>>(dbEstablishments);
            return establishments;
        }

        public ItemResponse GetEventDetail(int id, GetDetailFilter filter)
        {
            var dbEventDetail = _itemRepository.GetEventDetail(id, filter.LanguageCode);
            var eventDetail = _mapper.Map<ItemResponse>(dbEventDetail);
            return eventDetail;
        }

        public List<ListItemResponse> GetEvents(GetListFilter filter, PaginationFilter pagination)
        {
            var dbEvents = _itemRepository.GetEvents(pagination.PageNumber, pagination.PageSize, filter.Name, filter.City);
            var events = _mapper.Map<List<ListItemResponse>>(dbEvents);
            return events;
        }

        public int GetEstablishmentsTotalCount(GetListFilter filter)
        {
            var totalCount = _itemRepository.GetEstablishmentCount(filter.Name, filter.City);
            return totalCount;
        }

        public int GetEventTotalCount(GetListFilter filter)
        {
            var totalCount = _itemRepository.GetEventCount(filter.Name, filter.City);
            return totalCount;
        }
    }
}
