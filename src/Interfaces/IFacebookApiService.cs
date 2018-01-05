using System.Threading.Tasks;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces
{
    /// <summary>
    /// The facebook api service interface.
    /// </summary>
    public interface IFacebookApiService
    {
        /// <summary>
        /// Verifies user token and returns information about user if found.
        /// </summary>
        /// <param name="token">
        /// The token.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IFacebookUser> VerifyUserTokenAsync(string token);
    }
}