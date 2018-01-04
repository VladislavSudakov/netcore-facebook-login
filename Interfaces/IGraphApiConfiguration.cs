﻿namespace SimpleSoft.AspNetCore.FacebookGraphApi.Interfaces
{
    /// <summary>
    /// The facebook api configuration interface.
    /// </summary>
    public interface IGraphApiConfiguration
    {
        /// <summary>
        /// Gets the application id. Should be provided.
        /// </summary>
        string ApplicationId { get; }

        /// <summary>
        /// Gets the application secret. Should be provided.
        /// </summary>
        string ApplicationSecret { get; }

        /// <summary>
        /// Gets the profile picture width. Optional. Leave null here if you dont know why you need it.
        /// </summary>
        int? ProfilePictureWidth { get; }

        /// <summary>
        /// Gets the profile picture height. Optional. Leave null here if you dont know why you need it.
        /// </summary>
        int? ProfilePictureHeight { get; }

        /// <summary>
        /// Gets the api url. Optional, by default uses 2.11 version graph api.
        /// </summary>
        string ApiUrl { get; }
    }
}