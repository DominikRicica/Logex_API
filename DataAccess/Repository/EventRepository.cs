using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class EventRepository : ItemRepository, IEventRepository
    {
        public EventRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public IEnumerable<ItemEntity> GetEvents(int pageNumber, int limit = 100, string filterName = null, string filterCity = null)
        {
            return ListItems((int)ItemType.Event, filterName, filterCity, pageNumber, limit);
        }

        public ItemEntity GetEventDetail(int id, string languageCode = null)
        {
            return GetItemDetails((int)ItemType.Event, id, languageCode);
        }

        public int GetEventCount(string filterName = null, string filterCity = null)
        {
            return GetItemCount((int)ItemType.Event, filterName, filterCity);
        }

        public IEnumerable<ItemEntity> GetEventsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius)
        {
            return GetItemsInRadius((int)ItemType.Event, latitude, longitude, radius, earthRadius);
        }
    }
}
