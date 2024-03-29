﻿using UI.Library.Models;

namespace UI.Library.API
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }

        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfoAsync(string token);
        void LogOffUser();
    }
}