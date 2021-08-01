using System.Collections.Generic;
using System.Net.Mime;
using Domain.Primary.Entities;

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

        /// <summary>
        /// The collection of allowed content types for the URL, stored in the ad of type <see cref="AdType.BannerAd"/>.
        /// </summary>
        public static readonly IEnumerable<string> BannerAdUrlAllowedContentTypes = new[]
            {MediaTypeNames.Image.Gif, MediaTypeNames.Image.Jpeg};

        /// <summary>
        /// Determines the minimum count of items, stored in the CategoryIds-array.
        /// </summary>
        public static readonly int CategoryIdsMinLength = 1;
    }
}