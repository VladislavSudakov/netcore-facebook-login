using System;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Exceptions
{
    /// <summary>
    /// The facebook api exception. Occurs if token verification completed with error
    /// or if any request failed to complete.
    /// </summary>
    public class FacebookApiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookApiException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public FacebookApiException(string message) : base(message)
        {
        }
    }
}