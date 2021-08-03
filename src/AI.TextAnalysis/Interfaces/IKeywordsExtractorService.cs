using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AI.TextAnalysis.Interfaces
{
    /// <summary>
    /// The service, that allows to extract keywords from a text.
    /// </summary>
    public interface IKeywordsExtractorService
    {
        /// <summary>
        /// Extracts keywords from the specified <paramref name="source"/>.
        /// </summary>
        Task<IEnumerable<string>> GetKeywordsFromStringAsync(string source, CancellationToken token);
    }
}