using DataAccess.Entities;
using DataAccess.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class EstablishmentRepository : ItemRepository, IEstablishmentRepository
    {
        public EstablishmentRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public IEnumerable<ItemEntity> GetEstablishments(int pageNumber, int limit = 100, string filterName = null, string filterCity = null)
        {
            return ListItems((int)ItemType.Establishment, filterName, filterCity, pageNumber, limit);
        }

        public ItemEntity GetEstablishmentDetail(int id, string languageCode = null)
        {
            return GetItemDetails((int)ItemType.Establishment, id, languageCode);
        }

        public int GetEstablishmentCount(string filterName = null, string filterCity = null)
        {
            return GetItemCount((int)ItemType.Establishment, filterName, filterCity);
        }

        public IEnumerable<ItemEntity> GetAllEstablishments()
        {
            return ListAll((int)ItemType.Establishment);
        }
    }
}
