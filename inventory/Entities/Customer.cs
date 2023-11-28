using System;

namespace inventory.Entities;

public class Customer : DateTimeInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Alamat { get; set; }
    public required string Email { get; set; }
    public required string Telepon { get; set; }
}