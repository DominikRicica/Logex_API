using Presentation.Filters;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public interface IEstablishmentService
    {
        List<ListItem> GetEstablishments(GetListFilter filter, PaginationFilter pagination);
        Item GetEstablishmentDetail(int id, GetDetailFilter filter);
        int GetEstablishmentsTotalCount(GetListFilter filter);
        List<ListItem> GetEventsInRadius(int establishmentId);
    }
}
