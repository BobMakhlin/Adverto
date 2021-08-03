using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AI.TextAnalysis.Interfaces;
using AI.TextAnalysis.Options;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;

namespace AI.TextAnalysis.Realisations.KeywordExtractor
{
    /// <summary>
    /// The service, that allows to extract keywords from a text,
    /// working inside with Azure Cognitive Services.
    /// </summary>
    public class AzureKeywordsExtractorService : IKeywordsExtractorService
    {
        #region Fields

        private TextAnalyticsClient _client;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public AzureKeywordsExtractorService(IConfiguration configuration)
        {
            _configuration = configuration;
            
            InitClient();
        }

        #endregion

        #region IKeywordsExtractorService

        public async Task<IEnumerable<string>> GetKeywordsFromStringAsync(string source, CancellationToken token)
        {
            Response<KeyPhraseCollection> response =
                await _client.ExtractKeyPhrasesAsync(source, cancellationToken: token)
                    .ConfigureAwait(false);
            return response.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the field <see cref="_client"/>.
        /// </summary>
        private void InitClient()
        {
            string endpoint = _configuration[AzureTextAnalysisApiOptions.EndpointConfigurationKey];
            string apiKey = _configuration[AzureTextAnalysisApiOptions.ApiKeyConfigurationKey];

            _client = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
        }

        #endregion
    }
}