using System;
using System.Collections.Generic;

namespace Domain.Primary.Entities
{
    public class Category
    {
        public Guid CategoryId { get; set; }
        public string Title { get; set; }

        public List<Ad> Ads { get; set; }
    }
}