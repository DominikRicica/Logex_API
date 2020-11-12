using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetEstablishments(int offset, int limit = 100, string filterName = null, string filterCity = null);
        IEnumerable<Item> GetEvents(int offset, int limit = 100, string filterName = null, string filterCity = null);
        Item GetEstablishmentDetail(int id, string languageCode);
        Item GetEventDetail(int id, string languageCode);
        int GetEstablishmentCount(string filterName = null, string filterCity = null);
        int GetEventCount(string filterName = null, string filterCity = null);
        IEnumerable<Item> GetEstablishmentsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius);
        IEnumerable<Item> GetEventsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius);
    }
}
