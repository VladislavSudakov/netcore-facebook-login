using System;
using SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces;
using SimpleSoft.AspNetCore.FacebookGraphApi.Models;

namespace SimpleSoft.AspNetCore.FacebookGraphApi.Validators
{
    internal static class DebugTokenValidator
    {
        internal static bool Validate(IGraphApiConfiguration config, DebugTokenResponse model)
        {
            DateTimeOffset expiredAtOffset = DateTimeOffset.FromUnixTimeSeconds(model.ExpiresAt);
            DateTime expiredAtUtc = expiredAtOffset.UtcDateTime;

            if (model.IsTokenValid && model.AppId == config.ApplicationId && DateTime.UtcNow < expiredAtUtc)
            {
                return true;
            }

            return false;
        }
    }
}
