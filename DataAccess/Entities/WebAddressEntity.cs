using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class WebAddressEntity
    {
        public int WebAddressID { get; set; }
        public int ItemId { get; set; }
        public string Url { get; set; }
    }
}
