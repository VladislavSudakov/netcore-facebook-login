using System;
using SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces;
using SimpleSoft.AspNetCore.FacebookGraphApi.Models;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Mappers
{
    internal static class FacebookUserMapper
    {
        internal static IFacebookUser MapUser(UserProfileResponse model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new FacebookUser
                       {
                           UserId = model.UserId,
                           ImageUrl = model.ImageUrl,
                           Email = model.Email,
                           FullName = model.Name,
                           IsValid = true
                       };
        }
    }
}
