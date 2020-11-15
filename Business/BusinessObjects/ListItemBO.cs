using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BusinessObjects
{
    public class ListItemBO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<DescriptionBO> Descriptions { get; set; }
    }
}
