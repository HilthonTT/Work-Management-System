using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.Models;

public class CreateItemModel
{
    public int Id { get; set; }

    [Required]
    public string ModelName { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required]
    public string Location { get; set; }

    [DisplayName("Internal Supplier User")]
    public string? InternalSupplierPersonId { get; set; }

    [DisplayName("Internal Supplier Company")]
    public int? InternalSupplierCompanyId { get; set; }

    [DisplayName("European Article Number")]
    public decimal? EAN { get; set; }
    public bool Archived { get; set; }
}
