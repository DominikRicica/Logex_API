using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class PagedResponse
    {
        public PagedResponse() { }

        public int? PagesTotal { get; set; }
        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
