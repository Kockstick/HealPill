using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class ClientsTab : Border
{
    private List<Treatment> treatments = new List<Treatment>();
    
    public ClientsTab()
    {
        InitializeComponent();
        treatments = DB.GetAllTreatments();
        Treatments_DataGrid.ItemsSource = treatments;
    }

    private void AppointmentBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow.Tab = new MakeAppointmentTab();
    }

    private void CanselBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        var treatment = Treatments_DataGrid.SelectedItem as Treatment;
        DB.Delete(treatment);
        treatments.Remove(treatment);
        Treatments_DataGrid.ItemsSource = treatments;
    }

    private void Treatments_DataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        MainWindow.Tab = new ClientTab(Treatments_DataGrid.SelectedItem as Treatment);
    }
}