using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class MedCardTab : Border
{
    private MedCard medCard { get; set; }
    
    public MedCardTab(MedCard medCard)
    {
        InitializeComponent();
        this.medCard = medCard;

        Name_TextBox.Text = medCard.Name;
        Phone_TextBox.Text = medCard.Phone;
        DateOfBorn_DatePicker.SelectedDate = medCard.DateOfBorn;

        Treatments_DataGrid.ItemsSource = DB.GetTreatmentsByMedCard(medCard);
    }

    private void SaveBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        medCard.Name = Name_TextBox.Text;
        medCard.Phone = Phone_TextBox.Text;
        DateTime.TryParse(DateOfBorn_DatePicker.SelectedDate.ToString(), out DateTime date);
        medCard.DateOfBorn = date;
        
        DB.Update(medCard);

        MainWindow.Tab = new MedCardsTab();
    }

    private void CanselBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow.Tab = new MedCardsTab();
    }
}