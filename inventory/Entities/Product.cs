using System;

namespace inventory.Entities;

public class Product : DateTimeInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Image { get; set; }
    public required string Qty { get; set; }
    public Category? Category { get; set; }
    public required int CategoryID { get; set; }
}