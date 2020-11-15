using Business.BusinessObjects;
using Business.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface IEventService
    {
        List<ListItemBO> GetEvents(GetListFilter filter, PaginationFilter pagination);
        ItemBO GetEventDetail(int id, GetDetailFilter filter);
        int GetEventTotalCount(GetListFilter filter);
        List<ListItemBO> GetEstablishmentInRadius(int eventId, int radius);
    }
}
