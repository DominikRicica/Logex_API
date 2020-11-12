using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Description> Descriptions { get; set; }
        public List<Media> Medias { get; set; }
        public List<WebAddress> WebAddresses { get; set; }
        public Item()
        {
            Descriptions = new List<Description>();
            Medias = new List<Media>();
            WebAddresses = new List<WebAddress>();
        }
    }
}
