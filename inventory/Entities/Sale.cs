using System;
using inventory.Entities;

public class Sale : DateTimeInfo
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Alamat { get; set; }
    public required string Email { get; set; }
    public required string Telepon { get; set; }
}