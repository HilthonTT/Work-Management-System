using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Library.Models;

namespace UI.Library.API;

public class UserEndpoint : IUserEndpoint
{
    private readonly IAPIHelper _apiHelper;
    private readonly ILogger<UserEndpoint> _logger;

    public UserEndpoint(IAPIHelper apiHelper,
                     ILogger<UserEndpoint> logger)
    {
        _apiHelper = apiHelper;
        _logger = logger ?? NullLogger<UserEndpoint>.Instance;
    }

    public async Task<List<UserModel>> GetAll()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllUsers");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<UserModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }


    public async Task<List<UserModel>> GetById(string Id)
    {
        var data = new
        {
            Id
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/GetAllUsers", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<List<UserModel>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<Dictionary<string, string>> GetAllRoles()
    {
        using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/User/Admin/GetAllRoles");
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task CreateUser(CreateUserModel model)
    {
        var data = new
        {
            model.FirstName,
            model.LastName,
            model.EmailAddress,
            model.PhoneNumber,
            model.DateOfBirth,
            model.DepartmentId,
            model.JobTitleId,
            model.Password
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Register", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("A new user of name {FirstName} {LastName} has been created",
                data.FirstName, data.LastName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateUser(UserModel model)
    {
        var data = new
        {
            model.FirstName,
            model.LastName,
            model.EmailAddress,
            model.PhoneNumber,
            model.DateOfBirth,
            model.DepartmentId,
            model.JobTitleId,
        };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/UpdateUser", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The user of name {firstName} {lastName} of {userId} has been updated", data.FirstName, data.LastName, model.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task AddUserToRole(string userId, string roleName)
    {
        var data = new { userId, roleName };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/AddUserToRole", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The user of Id {userId} has gained the role of name {roleName}", data.userId, data.roleName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task RemoveUserFromRole(string userId, string roleName)
    {
        var data = new { userId, roleName };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/RemoveUserFromRole", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The user of Id {userId} has lost the role of name {roleName}", data.userId, data.roleName);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
