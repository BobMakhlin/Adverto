using System;

namespace Domain.Primary.Entities
{
    public class DisabledAd
    {
        public Guid AdId { get; set; }
        public Ad Ad { get; set; }

        public DateTime DisabledAt { get; set; }
    }
}