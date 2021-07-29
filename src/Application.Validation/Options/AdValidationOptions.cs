namespace Application.Validation.Options
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate an ad.
    /// </summary>
    internal static class AdValidationOptions
    {
        /// <summary>
        /// Determines the minimum value of the cost.
        /// </summary>
        public static readonly double CostMinValue = 0.1;

        /// <summary>
        /// Determines the minimum count of letters in content of the ad.
        /// </summary>
        public static readonly int ContentMinLength = 12;
        
        /// <summary>
        /// Determines the maximum count of letters in content of the ad.
        /// </summary>
        public static readonly int ContentMaxLength = 3600;
    }
}