using System.Collections.Generic;
using Mainwave.MimeTypes;

namespace Application.Validation.Options.Ad
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a banner-ad.
    /// </summary>
    public static class BannerAdValidationOptions
    {
        /// <summary>
        /// The collection of allowed content types for the URL.
        /// </summary>
        public static readonly IEnumerable<string> UrlAllowedContentTypes = new[]
        {
            MimeType.Image.Gif, 
            MimeType.Image.Jpeg
        };
    }
}