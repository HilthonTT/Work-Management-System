namespace WSMApi.Models;

public class ApplicationUserModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? DepartmentId { get; set; }
    public int? JobTitleId { get; set; }
    public DateTime CreatedDate { get; set; }

    public Dictionary<string, string> Roles { get; set; } = new();
}
