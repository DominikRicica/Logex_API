using Presentation.Filters;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public interface IEventService
    {
        List<ListItem> GetEvents(GetListFilter filter, PaginationFilter pagination);
        Item GetEventDetail(int id, GetDetailFilter filter);
        int GetEventTotalCount(GetListFilter filter);
        List<ListItem> GetEstablishmentInRadius(int eventId, int radius);
    }
}
