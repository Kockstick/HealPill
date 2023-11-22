using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class MakeAppointmentTab : Border
{
    public MakeAppointmentTab()
    {
        InitializeComponent();
        MedCards_ComboBox.ItemsSource = DB.GetAllMedCards();
    }

    private void SaveBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Treatment treatment = new Treatment();
        treatment.MedCard = MedCards_ComboBox.SelectedItem as MedCard;
        treatment.DateRequest = DateTime.Now;
        treatment.DateVisit = getLastVisit();
        treatment.Worker = DB.currentWorker;
        DB.New(treatment);

        MainWindow.Tab = new ClientsTab();
    }

    private DateTime getLastVisit()
    {
        var allTreatments = DB.GetAllTreatments();
        var lastTreatment = DateTime.Now;
        if(allTreatments.Count > 0)
            lastTreatment = allTreatments.Max(u => u.DateVisit);

        lastTreatment = lastTreatment.AddMinutes(DB.currentWorker.ServiceTime);
        return lastTreatment;
    }

    private void CanselBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow.Tab = new ClientsTab();
    }
}