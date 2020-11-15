using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class ItemEntity
    {
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<DescriptionEntity> Descriptions { get; set; }
        public List<MediaEntity> Medias { get; set; }
        public List<WebAddressEntity> WebAddresses { get; set; }
        public ItemEntity()
        {
            Descriptions = new List<DescriptionEntity>();
            Medias = new List<MediaEntity>();
            WebAddresses = new List<WebAddressEntity>();
        }
    }
}
