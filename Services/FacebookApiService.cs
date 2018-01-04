using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

using SimpleSoft.AspNetCore.FacebookGraphApi.Constants;
using SimpleSoft.AspNetCore.FacebookGraphApi.Exceptions;
using SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces;
using SimpleSoft.AspNetCore.FacebookGraphApi.Mappers;
using SimpleSoft.AspNetCore.FacebookGraphApi.Models;
using SimpleSoft.AspNetCore.FacebookGraphApi.Validators;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Services
{
    public class FacebookApiService : IFacebookApiService
    {
        private readonly IGraphApiConfiguration configuration;
        private readonly string apiUrl;

        public FacebookApiService(IGraphApiConfiguration configuration)
        {
            this.configuration = configuration;
            this.apiUrl = !string.IsNullOrEmpty(this.configuration.ApiUrl) ? this.configuration.ApiUrl : FacebookApiConstants.ApiUrl;

            // wrap the end slash char in case user forgot about it
            if (!this.apiUrl.EndsWith("/"))
            {
                this.apiUrl = $"{this.apiUrl}/";
            }
        }

        async Task<IFacebookUser> IFacebookApiService.VerifyUserTokenAsync(string token)
        {
            // 1. debug token request, checks that issue application id is the same as our application and token is not expired
            // 2. if token is valid - return profile of a user.            

            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException(nameof(token), "VerifyUserTokenAsync: Token provided should not be empty!");
            }

            using (HttpClient client = new HttpClient())
            {
                var appToken = await this.AppAccessTokenCallAsync(client);
                var isTokenValid = await this.DebugTokenCallAsync(client, token, appToken);

                if (!isTokenValid)
                {
                    // setting false here just to show that it is not valid.
                    return new FacebookUser { IsValid = false }; 
                }

                return await this.ProfileRequestAsync(client, token);
            }
        }

        private async Task<string> AppAccessTokenCallAsync(HttpClient client)
        {
            Uri appTokenUri = 
             new Uri($"{this.apiUrl}oauth/access_token?client_id={this.configuration.ApplicationId}&client_secret={this.configuration.ApplicationSecret}&grant_type=client_credentials");

            HttpResponseMessage response = await client.GetAsync(appTokenUri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                AccessTokenResponse appTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(content);

                return appTokenResponse.Token;
            }

            throw new FacebookApiException("AppAccessTokenCallAsync: the response is not succeded.");
        }

        private async Task<bool> DebugTokenCallAsync(HttpClient client, string clientToken, string appToken)
        {
            Uri debugTokenUri = new Uri($"{this.apiUrl}debug_token?input_token={clientToken}&access_token={appToken}");
            HttpResponseMessage response = await client.GetAsync(debugTokenUri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                DebugTokenResponse debugTokenResponse = JsonConvert.DeserializeObject<DebugTokenResponse>(content);

                return DebugTokenValidator.Validate(this.configuration, debugTokenResponse);
            }

            throw new FacebookApiException("DebugTokenCallAsync: the response is not succeded.");
        }

        private async Task<IFacebookUser> ProfileRequestAsync(HttpClient client, string token)
        {
            string pictureDimentions = string.Empty;
            if (this.configuration.ProfilePictureWidth.HasValue && this.configuration.ProfilePictureHeight.HasValue)
            {
                pictureDimentions =
                    $".width({this.configuration.ProfilePictureWidth.Value}).height({this.configuration.ProfilePictureHeight.Value})";
            }

            Uri profileUri = new Uri($"{this.apiUrl}me?access_token={token}&fields=email,name,picture{pictureDimentions}");
            HttpResponseMessage response = await client.GetAsync(profileUri);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                UserProfileResponse profileResponse = JsonConvert.DeserializeObject<UserProfileResponse>(content);

                return FacebookUserMapper.MapUser(profileResponse);
            }

            throw new FacebookApiException("ProfileRequestAsync: the response is not succeded.");
        }
    }
}
