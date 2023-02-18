using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.FormModels;

public class FormPartModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Part Name Is A Required Field.")]
    [DisplayName("Part Name")]
    public string PartName { get; set; }

    [Required(ErrorMessage = "Model Name Is A Required Field.")]
    [DisplayName("Model Name")]
    public string ModelName { get; set; }

    [Required(ErrorMessage = "Purchased Price Is A Required Field.")]
    [DisplayName("Purchased Price")]
    public decimal PurchasedPrice { get; set; }

    [DisplayName("Selected Machine")]
    public int? MachineId { get; set; }

    [Required(ErrorMessage = "Date Purchased Is A Required Field.")]
    [DisplayName("Date Purchased")]
    [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = "DateTime Must Be Between 01/01/1753 And 31/12/9999")]
    public DateTime DatePurchased { get; set; }
}
