using System;
using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.Tags.Models
{
    public class TagDto : IMapFrom<Tag>
    {
        #region Properties

        public Guid TagId { get; set; }
        public string Title { get; set; }

        #endregion
    }
}