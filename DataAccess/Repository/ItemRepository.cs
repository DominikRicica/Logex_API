using Dapper;
using DataAccess.Entities;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Repository
{
    public class ItemRepository : SqLiteBaseRepository, IItemRepository
    {
        public IEnumerable<Item> GetEstablishments(int offset, int limit = 100, string filterName = null, string filterCity = null)
        {
            return ListItems((int)ItemType.Establishment, filterName, filterCity, offset, limit);
        }

        public IEnumerable<Item> GetEvents(int offset, int limit = 100, string filterName = null, string filterCity = null)
        {
            return ListItems((int)ItemType.Event, filterName, filterCity, offset, limit);
        }

        public Item GetEstablishmentDetail(int id, string languageCode = null)
        {
            return GetItemDetails((int)ItemType.Establishment, id, languageCode);
        }

        public Item GetEventDetail(int id, string languageCode = null)
        {
            return GetItemDetails((int)ItemType.Event, id, languageCode);
        }

        public int GetEstablishmentCount(string filterName = null, string filterCity = null)
        {
            return GetItemCount((int)ItemType.Establishment, filterName, filterCity);
        }

        public int GetEventCount(string filterName = null, string filterCity = null)
        {
            return GetItemCount((int)ItemType.Event, filterName, filterCity);
        }

        public IEnumerable<Item> GetEstablishmentsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius)
        {
            return GetItemsInRadius((int)ItemType.Establishment, latitude, longitude, radius, earthRadius);
        }

        public IEnumerable<Item> GetEventsInRadius(decimal latitude, decimal longitude, int radius, int earthRadius)
        {
            return GetItemsInRadius((int)ItemType.Event, latitude, longitude, radius, earthRadius);
        }

        private int GetItemCount(int itemType, string filterName, string filterCity)
        {
            var p = new
            {
                ItemType = itemType,
                FilterName = '%' + filterName + '%',
                FilterCity = '%' + filterCity + '%',
            };

            using (var cnn = SimpleDbConnection())
            {
                var sql = @"SELECT COUNT(*) 
                            FROM Items
                            WHERE Items.ItemTypeId = @ItemType
                                AND Items.Name like @FilterName
                                AND Items.City like @FilterCity";
                var count = cnn.ExecuteScalar<int>(sql, p);
                return count;
            }
        }

        private IEnumerable<Item> ListItems(int itemType, string filterName, string filterCity, int offset, int limit)
        {
            var p = new
            {
                ItemType = itemType,
                FilterName = '%' + filterName + '%',
                FilterCity = '%' + filterCity + '%',
                Limit = limit,
                Offset = offset * limit
            };
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                var sql = @"SELECT Itms.*, Descriptions.*
                            FROM (SELECT * FROM Items
                                    WHERE Items.ItemTypeId = @ItemType
                                    AND Items.Name like @FilterName
                                    AND Items.City like @FilterCity
                                    ORDER BY Items.ItemId
                                    DESC Limit @Limit Offset @Offset) Itms
                            LEFT JOIN Descriptions ON Itm.ItemId = Descriptions.ItemId";

                var items = cnn.Query<Item, Description, Item>(sql, (item, description) => {
                    item.Descriptions.Add(description);
                    return item;
                }, p, splitOn: "DescriptionId");
                var result = items.GroupBy(i => i.ItemId).Select(g =>
                {
                    var groupedItem = g.First();
                    groupedItem.Descriptions = g.Select(i => i.Descriptions.Single()).ToList();
                    return groupedItem;
                }).ToList();
                return result;
            }
        }

        private Item GetItemDetails(int itemType, int id, string languageCode = null)
        {
            var p = new
            {
                ItemType = itemType,
                Id = id,
                LanguageCode = languageCode ?? "%"
            };
            if (!File.Exists(DbFile)) return null;

            using (var cnn = SimpleDbConnection())
            {
                var sql = @"SELECT Items.*, Descriptions.*, Media.*, WebAddresses.* 
                            FROM Items
                                LEFT JOIN Descriptions ON Items.ItemId = Descriptions.ItemId
                                LEFT JOIN Media ON Items.ItemId = Media.ItemId
                                LEFT JOIN WebAddresses ON Items.ItemId = WebAddresses.ItemId
                            WHERE Items.ItemTypeId = @ItemType
                                AND Items.ItemId = @Id
                                AND Descriptions.LanguageCode like @LanguageCode";
                var items = cnn.Query<Item, Description, Media, WebAddress, Item>(sql, (item, description, media, webAdress) => {
                    item.Descriptions.Add(description);
                    item.Medias.Add(media);
                    item.WebAddresses.Add(webAdress);
                    return item;
                }, p, splitOn: "DescriptionId, MediaId, WebAddressID");

                var result = items.FirstOrDefault();
                foreach (var item in items)
                {
                    if (!result.Descriptions.Select(d => d.DescriptionId).ToList().Contains(item.Descriptions.Single().DescriptionId))
                        result.Descriptions.Add(item.Descriptions.Single());
                    if (!result.Medias.Select(m => m.MediaId).ToList().Contains(item.Medias.Single().MediaId))
                        result.Medias.Add(item.Medias.Single());
                    if (!result.WebAddresses.Select(wa => wa.WebAddressID).ToList().Contains(item.WebAddresses.Single().WebAddressID))
                        result.WebAddresses.Add(item.WebAddresses.Single());
                }
                return result;
            };
        }

        private IEnumerable<Item> GetItemsInRadius(int itemType, decimal latitude, decimal longitude, int radius, int earthRadius)
        {
            var p = new
            {
                ItemType = itemType,
                Latitude = latitude,
                Longitude = longitude,
                Radius = radius,
                EarthRadius = earthRadius
            };
            using (var cnn = SimpleDbConnection())
            {
                var sql = @"SELECT *             
                            FROM Items
                            WHERE Items.ItemTypeId = @ItemType
                                AND 2 * @EarthRadius * asin(sqrt((sin(radians((@Latitude - Items.Latitude) / 2))) ^ 2 + cos(radians(Items.Latitude)) * cos(radians(@Latitude)) * (sin(radians((@Longitude - Items.Longitude) / 2))) ^ 2)) >= @Radius";
                var items = cnn.Query<Item>(sql,p);
                var result = items.ToList();
                return result;
            }       
        }
    }
}
