using System;

namespace HealPill.Database;

public class Treatment
{
    public int Id { get; set; }
    public Worker Worker { get; set; }
    public Disease Disease { get; set; }
    public DateTime DateRequest { get; set; }
    public DateTime DateVisit { get; set; }
    public DateTime DateGetRecept { get; set; }
    public DateTime DateEndHeal { get; set; }
    public MedCard MedCard { get; set; }
}