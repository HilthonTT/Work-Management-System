using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WSMApi.Library.Internal.DataAccess;
using WSMApi.Library.Models;

namespace WSMApi.Library.DataAccess;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _sql;

    public UserData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    public UserModel GetUserById(string Id)
    {
        var output = _sql.LoadData<UserModel, dynamic>("dbo.spUser_GetById", new { Id }, "WSMData");

        return output.FirstOrDefault();
    }

    public List<UserModel> GetUserByName(string FirstName, string LastName)
    {
        var output = _sql.LoadData<UserModel, dynamic>("dbo.spUser_GetByName", new { FirstName, LastName }, "WSMData");

        return output;
    }

    public List<UserModel> GetUsers()
    {
        var output = _sql.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { }, "WSMData");

        return output;
    }

    public void InsertUser(UserModel user)
    {
        _sql.SaveData("dbo.spUser_Insert", user, "WSMData");
    }

    public void UpdateUser(UserModel user)
    {
        _sql.SaveData("dbo.spUser_Update", user, "WSMData");
    }

    public void UpdateAge(UserModel user, int Age)
    {
        _sql.SaveData("dbo.spUser_UpdateAge", new { Id = user.Id, Age }, "WSMData");
    }

    public void UpdateJobTitleId(UserModel user)
    {
        _sql.SaveData("dbo.spUser_UpdateJobId", new { user.Id, user.JobTitleId }, "WSMData");
    }
}
