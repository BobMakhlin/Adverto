namespace AI.TextAnalysis.Options
{
    internal static class AzureTextAnalysisApiOptions
    {
        /// <summary>
        /// The configuration key, by which the endpoint is stored in the configuration.
        /// </summary>
        public static readonly string EndpointConfigurationKey = "Azure:CognitiveServices:TextAnalysis:Endpoint";
        /// <summary>
        /// The configuration key, by which the API-key is stored in the configuration.
        /// </summary>
        public static readonly string ApiKeyConfigurationKey = "Azure:CognitiveServices:TextAnalysis:ApiKey";
    }
}