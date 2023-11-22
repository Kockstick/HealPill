using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill.Widgets;

public partial class MedCardsTab : Border
{
    private List<MedCard> medCards = new List<MedCard>();
    
    public MedCardsTab()
    {
        InitializeComponent();
        medCards = DB.GetAllMedCards();
        MedCards_DataGrid.ItemsSource = medCards;
    }

    private void MedCards_DataGrid_OnDoubleTapped(object? sender, TappedEventArgs e)
    {
        MedCard medCard = MedCards_DataGrid.SelectedItem as MedCard;
        if(medCard == null)
            return;

        MainWindow.Tab = new MedCardTab(medCard);
    }
}