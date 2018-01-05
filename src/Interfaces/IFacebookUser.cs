namespace SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces
{
    /// <summary>
    /// The facebook user interface.
    /// </summary>
    public interface IFacebookUser
    {
        /// <summary>
        /// Gets the user id.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the image url.
        /// </summary>
        string ImageUrl { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets a value indicating whether token is valid.
        /// </summary>
        bool IsValid { get; }
    }
}