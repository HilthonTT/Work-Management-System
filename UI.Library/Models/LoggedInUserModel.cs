﻿namespace UI.Library.Models;
public class LoggedInUserModel : ILoggedInUserModel
{
    public string Token { get; set; }
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; }
    public int? DepartmentId { get; set; }
    public int? JobTitleId { get; set; }
    public DateTime CreatedDate { get; set; }

    public Dictionary<string, string> Roles { get; set; } = new();
    public List<JobTitleModel> JobTitles { get; set; } = new();

    public void ResetUserModel()
    {
        Token = "";
        Id = "";
        FirstName = "";
        LastName = "";
        EmailAddress = "";
        PhoneNumber = "";
        Age = 0;
        DepartmentId = 0;
        JobTitleId = 0;
        CreatedDate = DateTime.MinValue;
    }
}
