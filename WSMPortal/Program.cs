using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using WSMPortal.Authentication;
using UI.Library.API;
using UI.Library.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();