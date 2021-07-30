using System;
using Application.Common.Mappings.Interfaces;
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

        #endregion
    }
}