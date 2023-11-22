using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class MedicinesList : Border
{
    public delegate void MedicinesList_EventHandler(Medicines medicine);

    public MedicinesList_EventHandler OnSelectMedicine;
    
    public MedicinesList()
    {
        InitializeComponent();
        Medicines_DataGrid.ItemsSource = DB.GetAllMedicine();
    }

    private void InputElement_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        var med = Medicines_DataGrid.SelectedItem as Medicine;
        if(med == null)
            return;

        Medicines medicines = new Medicines();
        medicines.Medicine = med;
        
        if (OnSelectMedicine != null)
            OnSelectMedicine(medicines);
        IsVisible = false;
    }
}