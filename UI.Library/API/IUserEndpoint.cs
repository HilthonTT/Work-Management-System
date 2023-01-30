using UI.Library.Models;

namespace UI.Library.API
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string roleName);
        Task CreateRole(string roleName);
        Task CreateUser(CreateUserModel model);
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task<List<UserModel>> GetById(string Id);
        Task RemoveUserFromRole(string userId, string roleName);
        Task UpdateUser(UserModel model);
        Task UpdateUserJobTitleId(UserModel model);
    }
}