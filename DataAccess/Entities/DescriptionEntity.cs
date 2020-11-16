using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class DescriptionEntity
    {
        public int DescriptionId { get; set; }
        public int ItemId { get; set; }
        public string LanguageCode { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
