using AutoMapper;
using Business.BusinessObjects;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Mapping
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemEntity, ItemBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.City + "(" + src.ZipCode + ")" + src.Address))
                .ForMember(dest => dest.WebAddresses, opt => opt.MapFrom(src => src.WebAddresses.Select(wa => wa.Url)))
                .ForMember(dest => dest.ImageLinks, opt => opt.MapFrom(src => src.Medias.Select(m => m.Url)));
            CreateMap<DescriptionEntity, DescriptionBO>();

            CreateMap<ItemEntity, ListItemBO>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ItemId))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.City + "(" + src.ZipCode + ")" + src.Address));
        }
    }
}
