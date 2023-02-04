﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSMApi.Library.Models;

public class DepartmentModel
{
    public int Id { get; set; }
    public int CompanyId { get; set; }
    public string DepartmentName { get; set; }
    public string Address { get; set; }

    public string? ChairPersonId { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
