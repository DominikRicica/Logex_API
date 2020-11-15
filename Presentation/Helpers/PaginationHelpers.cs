using Presentation.Filters;
using Presentation.Models;
using Presentation.Queries;
using Presentation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Helpers
{
    public static class PaginationHelpers
    {
        public static PagedResponse CreatePaginatedResponse(PaginationFilter pagination, int totalRecords)
        {
            return new PagedResponse
            {
                PagesTotal = (int)Math.Ceiling((decimal)totalRecords / pagination.PageSize),
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?)null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?)null,
            };
        }
    }
}
