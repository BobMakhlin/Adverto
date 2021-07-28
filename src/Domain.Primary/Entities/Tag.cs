using System;
using System.Collections.Generic;

namespace Domain.Primary.Entities
{
    public class Tag
    {
        public Guid TagId { get; set; }
        public string Title { get; set; }

        public List<Ad> Ads { get; set; } = new List<Ad>();
    }
}