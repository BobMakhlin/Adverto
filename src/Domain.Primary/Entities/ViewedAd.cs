using System;

namespace Domain.Primary.Entities
{
    public class ViewedAd
    {
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}