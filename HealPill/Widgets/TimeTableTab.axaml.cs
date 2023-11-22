using System;
using System.Collections.Generic;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class TimeTableTab : Border
{
    public TimeTableTab()
    {
        InitializeComponent();
        
        foreach (var item in Enum.GetValues<DayWeek>())
        {
            DB.GetTimeTablesByDay(item.ToString(), out List<TimeTable> times);
            if (times.Count <= 0)
            {
                Days_StackPanel.Children.Add(new WorkTime(item.ToString()));
                continue;
            }
            
            Days_StackPanel.Children.Add(new WorkTime(times));
        }
    }
}