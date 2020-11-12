using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class ItemResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<string> WebAddresses { get; set; }
        public List<string> ImageLinks { get; set; }
        public List<DescriptionResponse> Descriptions { get; set; }
    }
}
