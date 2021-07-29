using System;
using Application.Common.Mappings.Interfaces;
using Domain.Primary.Entities;

namespace Application.CQRS.Categories.Model
{
    public class CategoryDto : IMapFrom<Category>
    {
        #region Properties

        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        #endregion
    }
}