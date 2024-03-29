﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UI.Library.Models;

namespace UI.Library.API;

public class APIHelper : IAPIHelper
{
    private HttpClient _apiClient;
    private readonly ILoggedInUserModel _loggedInUser;
    private readonly IConfiguration _config;

    public APIHelper(ILoggedInUserModel loggedInUser,
                  IConfiguration config)
    {
        _loggedInUser = loggedInUser;
        _config = config;
        InitializeClient();
    }

    public HttpClient ApiClient
    {
        get
        {
            return _apiClient;
        }
    }

    private void InitializeClient()
    {
        string api = _config.GetValue<string>("api");

        _apiClient = new();
        _apiClient.BaseAddress = new Uri(api);
        _apiClient.DefaultRequestHeaders.Accept.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<AuthenticatedUser> Authenticate(string username, string password)
    {
        var data = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string ,string>("password", password)
        });

        using HttpResponseMessage response = await _apiClient.PostAsync("/Token", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public void LogOffUser()
    {
        _apiClient.DefaultRequestHeaders.Clear();
    }

    public async Task GetLoggedInUserInfoAsync(string token)
    {
        _apiClient.DefaultRequestHeaders.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Clear();
        _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        using (HttpResponseMessage response = await _apiClient.GetAsync("/api/user/GetMyId"))
        {
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<LoggedInUserModel>();
                _loggedInUser.CreatedDate = result.CreatedDate;
                _loggedInUser.FirstName = result.FirstName;
                _loggedInUser.LastName = result.LastName;
                _loggedInUser.Age = result.Age;
                _loggedInUser.PhoneNumber = result.PhoneNumber;
                _loggedInUser.JobTitleId = result.JobTitleId;
                _loggedInUser.DepartmentId = result.DepartmentId;
                _loggedInUser.EmailAddress = result.EmailAddress;
                _loggedInUser.Roles = result.Roles;
                _loggedInUser.JobTitles = result.JobTitles;
                _loggedInUser.Id = result.Id;
                _loggedInUser.Token = token;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }

}
