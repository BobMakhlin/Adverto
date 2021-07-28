using System;
using System.Collections.Generic;

namespace Domain.Primary.Entities
{
    public class Ad
    {
        public Guid AdId { get; set; }
        public AdType AdType { get; set; }
        public double Cost { get; set; }
        public string Content { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Category> Categories { get; set; } = new List<Category>();

        public ViewedAd ViewedAd { get; set; }
        public DisabledAd DisabledAd { get; set; }
    }
}