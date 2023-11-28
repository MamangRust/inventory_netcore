using System;

namespace inventory.Entities;

public class ProductKeluar : DateTimeInfo
{
    public int Id { get; set; }
    public required string Qty { get; set; }
    public Product? Product { get; set; }
    public required int ProductID { get; set; }
    public Category? Category { get; set; }
    public required int CategoryID { get; set; }
}