using DataAccess.Entities;
using Presentation.Filters;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public interface IItemService
    {
        List<ListItemResponse> GetEstablishments(GetListFilter filter, PaginationFilter pagination);
        List<ListItemResponse> GetEvents(GetListFilter filter, PaginationFilter pagination);
        ItemResponse GetEstablishmentDetail(int id, GetDetailFilter filter);
        ItemResponse GetEventDetail(int id, GetDetailFilter filter);
        public int GetEstablishmentsTotalCount(GetListFilter filter);
        public int GetEventTotalCount(GetListFilter filter);
    }
}
