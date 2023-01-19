﻿using UI.Library.Models;

namespace UI.Library.API
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string roleName);
        Task CreateUser(CreateUserModel model);
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task RemoveUserFromRole(string userId, string roleName);
    }
}