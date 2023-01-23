using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class UserModel
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int DepartmentId { get; set; }
    public int JobTitleId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
