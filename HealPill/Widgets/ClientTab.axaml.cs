using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class ClientTab : Border
{
    private Treatment treatment;
    private MedCard medCard;
    
    public ClientTab(Treatment treatment)
    {
        InitializeComponent();
        MedicinesList.OnSelectMedicine += OnAddMedicine;

        this.treatment = treatment;
        medCard = treatment.MedCard;

        Name_Label.Content = medCard.Name;
        DateOfBorn_Label.Content = medCard.DateOfBorn.ToShortDateString();
        Phone_Label.Content = medCard.Phone;

        DateRequest_Label.Content += treatment.DateRequest.ToShortDateString();
        DateVisit_Label.Content += treatment.DateVisit.ToShortDateString();

        Diseases_ComboBox.ItemsSource = DB.GetAllDiseases();

        var lastTreatments = DB.GetTreatmentsByMedCard(medCard);
        lastTreatments.Remove(treatment);
        Treatments_DataGrid.ItemsSource = lastTreatments;
        
        this.treatment.DateVisit = DateTime.Now;
    }

    private void OnAddMedicine(Medicines medicines)
    {
        medicines.Treatment = treatment.Id;
        DB.New(medicines);
        UpdateMediciners();
    }

    private void SaveBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        treatment.DateGetRecept = DateTime.Now;
        treatment.Disease = Diseases_ComboBox.SelectedItem as Disease;
        DateTime.TryParse(DateEndHeal_TimePicker.SelectedDate.ToString(), out DateTime date);
        treatment.DateEndHeal = date;
        
        DB.Update(treatment);
        MainWindow.Tab = new ClientsTab();
    }

    private void CanselBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        treatment.DateVisit = new DateTime();
        MainWindow.Tab = new ClientsTab();
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        MedicinesList.IsVisible = true;
    }

    private void RemoveBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        Medicines med = Medicines_DataGrid.SelectedItem as Medicines;
        if(med == null)
            return;
        
        DB.Delete(med);
        
        UpdateMediciners();
    }

    private void UpdateMediciners()
    {
        Medicines_DataGrid.ItemsSource = DB.GetMedicines(treatment);
    }
}