using System;

namespace HealPill.Database;

public class TimeTable
{
    public int Id { get; set; }
    public Worker Worker { get; set; }
    public string DayOfWeek { get; set; }
    public TimeOnly Start { get; set; }
    public TimeOnly End { get; set; }
}

public enum DayWeek
{
    Понедельник,
    Вторник,
    Среда,
    Четверг,
    Пятница,
    Суббота,
    Воскресенье
}