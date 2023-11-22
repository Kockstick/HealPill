using System;

namespace HealPill.Database;

public class Worker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public int ServiceTime { get; set; }
    public DateTime DateOfBorn { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}