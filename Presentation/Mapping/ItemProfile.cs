using AutoMapper;
using Business.BusinessObjects;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Mapping
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemBO, Item>();
            CreateMap<ListItemBO, ListItem>();
            CreateMap<DescriptionBO, Description>();
        }
    }
}
