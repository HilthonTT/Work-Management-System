using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public interface IUserData
{
    List<UserModel> GetUserById(string Id);
    List<UserModel> GetUserByName(string FirstName, string LastName);
    List<UserModel> GetUsers();
    void InsertUser(UserModel user);
    void UpdateAge(UserModel user, int Age);
    void UpdateJobTitleId(UserModel user);
    void UpdateUser(UserModel user);
}