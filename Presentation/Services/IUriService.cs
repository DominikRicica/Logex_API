using Presentation.Filters;
using Presentation.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public interface IUriService
    {
        Uri GetEstablishmentUri(int establishmentId, GetDetailQuery query = null);
        Uri GetAllEstablishmentsUri(PaginationQuery pagination = null);
    }
}
