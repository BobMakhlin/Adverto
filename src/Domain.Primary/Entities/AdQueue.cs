using System;

namespace Domain.Primary.Entities
{
    public class AdQueue
    {
        public Guid AdQueueId { get; set; }
        public int CurrentAdIndex { get; set; }
    }
}