using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Validation.Tools.Helpers
{
    public static class UrlValidationHelpers
    {
        /// <summary>
        /// Checks the content type of the remote file.
        /// </summary>
        /// <param name="url">The url, referencing a remote file</param>
        /// <param name="expectedContentTypes">The collection of expected content types</param>
        /// <returns>
        /// True: if the content type of the remote file is one of <paramref name="expectedContentTypes"/>.
        /// False: if the remote file doesn't exist or if its content type is not one of <paramref name="expectedContentTypes"/>.
        /// </returns>
        public static async Task<bool> RemoteFileContentTypeEqualsToAsync(string url,
            IEnumerable<string> expectedContentTypes)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Head;

                using WebResponse webResponse = await request.GetResponseAsync()
                    .ConfigureAwait(false);
                HttpWebResponse httpWebResponse = webResponse as HttpWebResponse;

                return httpWebResponse?.StatusCode == HttpStatusCode.OK
                       && expectedContentTypes.Contains(httpWebResponse.ContentType);
            }
            catch
            {
                return false;
            }
        }
    }
}