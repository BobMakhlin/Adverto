using System;
using System.Collections.Generic;

namespace Application.Queues.Interfaces
{
    /// <summary>
    /// Represents the iterator of the ad queue.
    /// The method <see cref="IAdQueueEnumerator.MoveNextAsync"/> returns the ID of the current ad.
    /// </summary>
    public interface IAdQueueEnumerator : IAsyncEnumerator<Guid>
    {
        
    }
}