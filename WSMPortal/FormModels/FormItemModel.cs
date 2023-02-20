using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WSMPortal.FormModels;

public class FormItemModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Model Name Is A Required Field.")]
    [DisplayName("Model Name")]
    public string ModelName { get; set; }

    [Required(ErrorMessage = "Description Is A Required Field.")]
    [DisplayName("Description")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Quantity Is A Required Field.")]
    [DisplayName("Quantity")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "Price Is A Required Field.")]
    [DisplayName("Price")]
    public decimal Price { get; set; }

    [DisplayName("European Article Number")]
    public decimal? EAN { get; set; }
    public bool Archived { get; set; }
}
