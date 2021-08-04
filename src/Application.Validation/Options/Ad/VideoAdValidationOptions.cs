using System.Collections.Generic;
using Mainwave.MimeTypes;

namespace Application.Validation.Options.Ad
{
    /// <summary>
    /// Contains <see langword="static readonly"/> fields, used to validate a video-ad.
    /// </summary>
    public static class VideoAdValidationOptions
    {
        /// <summary>
        /// The collection of allowed content types for the URL.
        /// </summary>
        public static readonly IEnumerable<string> UrlAllowedContentTypes = new[]
        {
            MimeType.Video.Threegpp, 
            MimeType.Video.H264, 
            MimeType.Video.Mp4, 
            MimeType.Video.Mpeg,
            MimeType.Video.Ogg,
            MimeType.Video.Quicktime,
            MimeType.Video.Webm
        };
    }
}