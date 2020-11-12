using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Queries
{
    public class GetListQuery
    {
        public GetListQuery()
        {
        }
        public GetListQuery(string name, string city)
        {
            City = city;
            Name = name;
        }

        [FromQuery(Name = "name")]
        public string Name { get; set; }

        [FromQuery(Name = "city")]
        public string City { get; set; }
    }
}
