using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public interface IEstablishmentRepository : IItemRepository
    {
        IEnumerable<ItemEntity> GetEstablishments(int pageNumber, int limit = 100, string filterName = null, string filterCity = null);
        ItemEntity GetEstablishmentDetail(int id, string languageCode = null);
        int GetEstablishmentCount(string filterName = null, string filterCity = null);
        IEnumerable<ItemEntity> GetAllEstablishments();
    }
}
