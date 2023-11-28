using System;

namespace inventory.Entities;

public class ProductMasuk : DateTimeInfo
{

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Qty { get; set; }
    public Product? Product { get; set; }
    public required int ProductID { get; set; }
    public Supplier? Supplier { get; set; }
    public required int SupplierID { get; set; }
}