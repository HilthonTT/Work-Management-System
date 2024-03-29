﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using UI.Library.API;
using UI.Library.Models;
using WSMPortal.Authentication;
using WSMPortal.Helpers;

namespace WSMPortal;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<ItemEndpoint>>();

        // Services
        builder.Services.AddMemoryCache();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();
        builder.Services.AddBlazoredLocalStorage();

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "RedisWSM_Dev";
        });

        // Authentication
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddAuthentication();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        // Personal Services
        builder.Services.AddSingleton<IAPIHelper, APIHelper>();
        builder.Services.AddSingleton<ILoggedInUserModel, LoggedInUserModel>();
        builder.Services.AddSingleton<PageHistoryState>();

        builder.Services.AddTransient<ICompanyEndpoint, CompanyEndpoint>();
        builder.Services.AddTransient<IDepartmentEndpoint, DepartmentEndpoint>();
        builder.Services.AddTransient<IJobTitleEndpoint, JobTitleEndpoint>();
        builder.Services.AddTransient<ITaskEndpoint, TaskEndpoint>();
        builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();
        builder.Services.AddTransient<IItemEndpoint, ItemEndpoint>();
        builder.Services.AddSingleton<ILogger, Logger<ItemEndpoint>>();
        builder.Services.AddTransient<IReportEndpoint, ReportEndpoint>();
    }
}
