using System;

namespace WSMDesktop.Models;

public class ItemDisplayModel
{
    public int Id { get; set; }
    public string ModelName { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal? EAN { get; set; }
    public bool Archived { get; set; }

    public string DisplayText
    {
        get
        {
            return $"{ModelName} - {Price}€";
        }
    }
}
