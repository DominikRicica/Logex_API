using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public interface IEventRepository : IItemRepository
    {
        IEnumerable<ItemEntity> GetEvents(int pageNumber, int limit = 100, string filterName = null, string filterCity = null);
        ItemEntity GetEventDetail(int id, string languageCode = null);
        int GetEventCount(string filterName = null, string filterCity = null);
        IEnumerable<ItemEntity> GetEventsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius);
    }
}
