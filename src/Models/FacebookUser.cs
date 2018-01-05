using SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Models
{
    internal class FacebookUser : IFacebookUser
    {
        public string UserId { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string Email { get; set; }

        public bool IsValid { get; set; }
    }
}