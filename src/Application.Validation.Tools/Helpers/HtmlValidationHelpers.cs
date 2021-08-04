using System.Linq;
using HtmlAgilityPack;

namespace Application.Validation.Tools.Helpers
{
    internal static class HtmlValidationHelpers
    {
        /// <summary>
        /// Checks if the specified <paramref name="htmlString"/> is valid.
        /// </summary>
        public static bool IsValidHtml(string htmlString)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);
            return !doc.ParseErrors.Any();
        } 
    }
}