using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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

    public async Task<List<UserModel>> GetAllAsync()
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


    public async Task<UserModel> GetByIdAsync(string Id)
    {
        var data = new { Id };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/GetById", data);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsAsync<UserModel>();
            return result;
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<Dictionary<string, string>> GetAllRolesAsync()
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

    public async Task CreateUserAsync(CreateUserModel model)
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

    public async Task UpdateUserAsync(UserModel model)
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

        using HttpResponseMessage response = await _apiHelper.ApiClient.PutAsJsonAsync("api/User/UpdateUser", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The user of name {firstName} {lastName} of {userId} has been updated", data.FirstName, data.LastName, model.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task UpdateUserJobTitleIdAsync(UserModel model)
    {
        var data = new { model.Id ,model.JobTitleId };

        using HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/UpdateUserJobId", data);
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("The user of {userId} has been updated", model.Id);
        }
        else
        {
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task CreateRoleAsync(string roleName)
    {
        var data = new { roleName };

        using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/User/Admin/CreateRole", data))
        {
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("The role of name {roleName} has been added to the database", roleName);
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }


    public async Task AddUserToRoleAsync(string userId, string roleName)
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

    public async Task RemoveUserFromRoleAsync(string userId, string roleName)
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
