namespace UI.Library.Models;

public class ItemModel
{
    /// <summary>
    /// The Identifier for the ItemModel.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the item.
    /// </summary>
    public string ModelName { get; set; }

    /// <summary>
    /// The description of the item
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// The quantity in stock of the item.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The average price it is sold at.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The location tells us where it is located in our warehouses.
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// It tells you who is the supplier for the item. It relates to the UserModel. Used when the Company is null
    /// </summary>
    public string? InternalSupplierPersonId { get; set; }

    /// <summary>
    /// It tells you who is the supplier for the item. It relates to the CompanyModel. Used when the User is null
    /// </summary>
    public int? InternalSupplierCompanyId { get; set; }

    /// <summary>
    /// The European Article Number.
    /// </summary>
    public decimal? EAN { get; set; }

    /// <summary>
    /// Checks if it is archived in the database. It is used for our fitlers.
    /// </summary>
    public bool Archived { get; set; }
}
