using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class CompanyModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string? ChairPersonId { get; set; }
    public string Description { get; set; }
    public DateTime DateFounded { get; set; } = DateTime.UtcNow;
    public bool Archived { get; set; } = false;
}
