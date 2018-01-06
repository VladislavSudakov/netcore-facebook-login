using System.Threading.Tasks;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces
{
    /// <summary>
    /// Provides methods for calling facebook graph api.
    /// </summary>
    public interface IFacebookApiService
    {
        /// <summary>
        /// Verifies user token if it was issues by valid appliaction, if it is not expired and if it is valid,
        /// returns information about user using the token if user found.
        /// </summary>
        /// <param name="token">
        /// The user access token. Get it after facebook login flow on client side.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IFacebookUser> VerifyUserTokenAsync(string token);
    }
}