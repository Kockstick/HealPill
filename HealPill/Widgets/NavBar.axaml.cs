using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace HealPill.Widgets;

public partial class NavBar : Grid
{
    private static NavBarBtn _clickedBtn;
    public static NavBarBtn clickedBtn
    {
        get => _clickedBtn;
        set
        {
            if(_clickedBtn != null)
                _clickedBtn.enabled = false;
            _clickedBtn = value;
            _clickedBtn.enabled = true;
        }
    }
    
    public NavBar()
    {
        InitializeComponent();
    }

    private void ClientsBtn_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        MainWindow.Tab = new ClientsTab();
    }

    private void TimeTableBtn_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        MainWindow.Tab = new TimeTableTab();
    }

    private void MebCardBtn_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        MainWindow.Tab = new MedCardsTab();
    }

    private void StatBtn_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        
    }

    private void ReportBtn_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        
    }
}