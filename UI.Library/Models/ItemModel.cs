﻿namespace UI.Library.Models;

public class ItemModel
{
    public int Id { get; set; }
    public string ModelName { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal? EAN { get; set; }
    public bool Archived { get; set; }
}
