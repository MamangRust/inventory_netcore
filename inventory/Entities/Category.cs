using System;

namespace inventory.Entities;

public class Category : DateTimeInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }

}
