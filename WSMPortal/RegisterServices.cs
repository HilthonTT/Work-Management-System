using AutoMapper;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using UI.Library.API;
using UI.Library.Models;
using WSMPortal.Authentication;
using WSMPortal.FormModels;

namespace WSMPortal;

public static class RegisterServices
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var logger = serviceProvider.GetService<ILogger<ItemEndpoint>>();

        builder.Services.AddMemoryCache();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        // Authentication
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthentication();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

        // Personal Services
        builder.Services.AddSingleton<IAPIHelper, APIHelper>();
        builder.Services.AddSingleton<ILoggedInUserModel, LoggedInUserModel>();

        builder.Services.AddTransient<ICompanyEndpoint, CompanyEndpoint>();
        builder.Services.AddTransient<IDepartmentEndpoint, DepartmentEndpoint>();
        builder.Services.AddTransient<IJobTitleEndpoint, JobTitleEndpoint>();
        builder.Services.AddTransient<ITaskEndpoint, TaskEndpoint>();
        builder.Services.AddTransient<IUserEndpoint, UserEndpoint>();
        builder.Services.AddTransient<IItemEndpoint, ItemEndpoint>();
        builder.Services.AddSingleton<ILogger, Logger<ItemEndpoint>>();

        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FormDepartmentModel, DepartmentModel>();
            cfg.CreateMap<FormCompanyModel, CompanyModel>();
            cfg.CreateMap<FormTaskModel, TaskModel>();
            cfg.CreateMap<DepartmentModel, FormDepartmentModel>();
            cfg.CreateMap<CompanyModel, FormCompanyModel>();
            cfg.CreateMap<TaskModel, FormTaskModel>();
        });

        var mapper = mapperConfiguration.CreateMapper();

        builder.Services.AddSingleton(mapper);
    }
}
