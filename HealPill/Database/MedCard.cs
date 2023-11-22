using System;

namespace HealPill.Database;

public class MedCard
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public DateTime DateOfBorn { get; set; }
}