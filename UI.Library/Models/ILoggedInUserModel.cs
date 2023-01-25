namespace UI.Library.Models
{
    public interface ILoggedInUserModel
    {
        int Age { get; set; }
        DateTime CreatedDate { get; set; }
        int? DepartmentId { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string Id { get; set; }
        int? JobTitleId { get; set; }
        string LastName { get; set; }
        string PhoneNumber { get; set; }
        string Token { get; set; }

        void ResetUserModel();
    }
}