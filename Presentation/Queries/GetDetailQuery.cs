using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Queries
{
    public class GetDetailQuery
    {
        [FromQuery(Name = "languageCode")]
        public string LanguageCode { get; set; }
    }
}