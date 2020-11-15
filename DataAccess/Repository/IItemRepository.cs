using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IItemRepository
    {
        IEnumerable<ItemEntity> ListItems(int itemType, string filterName, string filterCity, int pageNumber, int limit);
        ItemEntity GetItemDetails(int itemType, int id, string languageCode = null);
        int GetItemCount(int itemType, string filterName, string filterCity);
        IEnumerable<ItemEntity> GetItemsInRadius(int itemType, decimal latitude, decimal longitude, int radius, int earthRadius);
    }
}
