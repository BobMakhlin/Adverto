using System;

namespace Domain.Primary.Entities
{
    public class AdView
    {
        public Guid AdViewId { get; set; }
        
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }

        public DateTime ViewedAt { get; set; }
    }
}