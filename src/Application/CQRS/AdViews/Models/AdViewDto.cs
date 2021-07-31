using System;
using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.AdViews.Models
{
    public class AdViewDto : IMapFrom<AdView>
    {
        #region Properties

        public DateTime ViewedAt { get; set; }

        #endregion
    }
}