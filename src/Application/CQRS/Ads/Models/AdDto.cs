using System;
using System.Collections.Generic;
using Application.Common.Mappings.Interfaces;
using Application.CQRS.AdViews.Models;
using Application.CQRS.Categories.Model;
using Application.CQRS.Tags.Models;
using AutoMapper;
using Domain.Primary.Entities;

namespace Application.CQRS.Ads.Models
{
    public class AdDto : IMapFrom<Ad>
    {
        #region Properties

        public Guid AdId { get; set; }
        public AdType AdType { get; set; }
        public double Cost { get; set; }
        public string Content { get; set; }
        public List<TagDto> Tags { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public List<AdViewDto> AdViews { get; set; }
        public bool IsDisabled { get; set; }

        #endregion

        #region IMapFrom<Ad>

        public void MapUsingProfile(Profile profile)
        {
            profile.CreateMap<Ad, AdDto>()
                .ForMember
                (
                    adDto => adDto.IsDisabled,
                    opts => opts.MapFrom(ad => ad.DisabledAd != null)
                );

            profile.CreateMap<Tag, TagDto>();
            profile.CreateMap<Category, CategoryDto>();
            profile.CreateMap<AdView, AdViewDto>();
        }

        #endregion
    }
}