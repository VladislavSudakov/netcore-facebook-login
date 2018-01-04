# netcore-facebook-login

[![Build status](https://ci.appveyor.com/api/projects/status/pjxh5g91jpbh7t84?svg=true)](https://ci.appveyor.com/project/Vladislav56947/netcore-facebook-login)
[![nuget](https://img.shields.io/nuget/v/SimpleSoft.AspNetCore.FacebookGraphApi.svg)](https://www.nuget.org/packages/SimpleSoft.AspNetCore.FacebookGraphApi/)

[![Build history](https://buildstats.info/appveyor/chart/Vladislav56947/netcore-facebook-login)](https://ci.appveyor.com/project/Vladislav56947/netcore-facebook-login/history)


The server side flow for facebook external login.  Verifies facebook access token of a user and gets information about user profile.


## Installation
`Install-Package SimpleSoft.AspNetCore.FacebookGraphApi`

## Configuration
Derive your settings file from `IGraphApiConfiguration` or create a new settings file. I prefer to create my own interfaces instead of using .net core internal ones,
so in my implementation it looks like this:

```c#
public class AppSettings : IGraphApiConfiguration {

     private static IConfiguration _configuration;
     protected AppSettings()
     {
     }

     public static void Initialize(IConfiguration configuration)
     {
        if (_configuration == null)
        {
            _configuration = configuration;
        }
    }
     
     public static AppSettings Instance { get; } = new AppSettings();

     public string ApplicationId => _configuration.GetValue<string>("Facebook:AppId");

     public string ApplicationSecret => _configuration.GetValue<string>("Facebook:AppSecret");

     public int? ProfilePictureWidth => _configuration.GetValue<int>("Facebook:PicWidth");

     public int? ProfilePictureHeight => _configuration.GetValue<int>("Facebook:PicHeight");

     public string ApiUrl { get; }
}
```

Add your dependency injections :

```c#
    services.AddSingleton<IGraphApiConfiguration>(AppSettings.AppSettings.Instance);
    services.AddSingleton<IFacebookApiService, FacebookApiService>();
```

## Usage

```c#
public class SocialLoginController
{
        private readonly IFacebookApiService facebookApiService;

        public SocialLoginController(IFacebookApiService facebookApiService)
        {
            this.facebookApiService = facebookApiService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody] SocialLoginView model)
        {
            var user = await this.facebookApiService.VerifyUserTokenAsync(model.SocialToken);
            // make something with your user information here..

            throw new NotImplementedException();
        }
}
```