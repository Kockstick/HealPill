using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class WorkTime : Border
{
    private List<TimeTable> times = new List<TimeTable>();
    
    public WorkTime()
    {
        InitializeComponent();
    }

    public WorkTime(string day) : this()
    {
        DayOfWeek_Label.Content = day;
    }

    public WorkTime(List<TimeTable> times) : this()
    {
        this.times = times;
        Times_DataGrid.ItemsSource = times;
        DayOfWeek_Label.Content = times[0].DayOfWeek;
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        TimeTable timeTable = new TimeTable();
    }

    private void DelBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}