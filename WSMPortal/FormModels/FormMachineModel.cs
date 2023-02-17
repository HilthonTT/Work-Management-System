using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WSMPortal.FormModels;

public class FormMachineModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Machine Name Is A Required Field.")]
    [DisplayName("Machine Name")]
    public string MachineName { get; set; }

    [Required(ErrorMessage = "Model Name Is A Required Field.")]
    [DisplayName("Model Name")]
    public string ModelName { get; set; }

    [Required(ErrorMessage = "Purchased Price Is A Required Field.")]
    [DisplayName("Purchased Price")]
    public decimal PurchasedPrice { get; set; }

    [Required(ErrorMessage = "European Article Number Is A Required Field.")]
    [DisplayName("European Article Number")]
    public string EuropeanArticleNumber { get; set; }

    [Required(ErrorMessage = "Date Purchased Is A Required Field.")]
    [DisplayName("Date Purchased")]
    [Range(typeof(DateTime), "01/01/1753", "31/12/9999", ErrorMessage = "DateTime Must Be Between 01/01/1753 And 31/12/9999")]
    public DateTime DatePurchased { get; set; }
}
