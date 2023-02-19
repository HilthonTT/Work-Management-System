using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMDesktop.Models;

public class CompanyDisplayModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string? ChairPersonId { get; set; }
    public string Description { get; set; }
    public DateTime DateFounded { get; set; }
    public bool Archived { get; set; }

    public string DisplayText
    {
        get
        {
            return $"{CompanyName} - ☎️{PhoneNumber}";
        }
    }
}
