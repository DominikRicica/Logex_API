using AutoMapper;
using DataAccess.Entities;
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
            CreateMap<ItemEntity, Item>()
                .ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Location,opt => opt.MapFrom(src => src.City + "(" + src.ZipCode + ")" + src.Address))
                .ForMember(dest => dest.WebAddresses,opt => opt.MapFrom(src => src.WebAddresses.Select(wa => wa.Url)))
                .ForMember(dest => dest.ImageLinks,opt => opt.MapFrom(src => src.Medias.Select(m => m.Url)));
            CreateMap<DescriptionEntity, Description>();

            CreateMap<ItemEntity, ListItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.City + "(" + src.ZipCode + ")" + src.Address));
        }
    }
}
