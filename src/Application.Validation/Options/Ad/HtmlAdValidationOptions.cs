using System.Collections.Generic;
using Mainwave.MimeTypes;

namespace Application.Validation.Options.Ad
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate an html-ad.
    /// </summary>
    public static class HtmlAdValidationOptions
    {
        /// <summary>
        /// The collection of allowed content types for the URL.
        /// </summary>
        public static readonly IEnumerable<string> UrlAllowedContentTypes = new[]
        {
            MimeType.Text.Html,
            $"{MimeType.Text.Html}; charset=utf-8"
        };
    }
}