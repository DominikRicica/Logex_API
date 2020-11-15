using Business.BusinessObjects;
using Business.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public interface IEstablishmentService
    {
        List<ListItemBO> GetEstablishments(GetListFilter filter, PaginationFilter pagination);
        ItemBO GetEstablishmentDetail(int id, GetDetailFilter filter);
        int GetEstablishmentsTotalCount(GetListFilter filter);
        List<ListItemBO> GetEventsInRadius(int establishmentId);
    }
}
