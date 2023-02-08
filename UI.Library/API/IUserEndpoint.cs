using UI.Library.Models;

namespace UI.Library.API
{
    public interface IUserEndpoint
    {
        Task AddUserToRoleAsync(string userId, string roleName);
        Task CreateRoleAsync(string roleName);
        Task CreateUserAsync(CreateUserModel model);
        Task<List<UserModel>> GetAllsAsync();
        Task<Dictionary<string, string>> GetAllRolesAsync();
        Task<List<UserModel>> GetByIdAsync(string Id);
        Task RemoveUserFromRoleAsync(string userId, string roleName);
        Task UpdateUserAsync(UserModel model);
        Task UpdateUserJobTitleIdAsync(UserModel model);
    }
}