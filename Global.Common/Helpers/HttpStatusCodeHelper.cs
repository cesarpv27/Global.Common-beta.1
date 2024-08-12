
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides common helper methods.
    /// </summary>
    public static class HttpStatusCodeHelper
    {
        /// <summary>
        /// Creates an instance of <see cref="HttpResponseMessage"/> with the specified <paramref name="httpStatusCode"/> and optional <paramref name="reasonPhrase"/>.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code of the response.</param>
        /// <param name="reasonPhrase">The reason phrase associated with the response (optional).</param>
        /// <returns>An instance of <see cref="HttpResponseMessage"/>.</returns>
        public static HttpResponseMessage CreateHttpResponseMessage(HttpStatusCode httpStatusCode, string? reasonPhrase = default)
        {
            return new HttpResponseMessage(httpStatusCode)
            {
                ReasonPhrase = reasonPhrase
            };
        }
    }
}
