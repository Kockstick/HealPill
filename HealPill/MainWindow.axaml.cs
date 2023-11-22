using Avalonia.Controls;

namespace HealPill;

public partial class MainWindow : Window
{
    private static MainWindow Instance { get; set; }

    private static Border _tab;
    public static Border Tab
    {
        get
        {
            return _tab;
        }
        set
        {
            Instance.StackPanel.Children.Remove(_tab);
            _tab = value;
            Instance.StackPanel.Children.Add(_tab);
        }
    }
    
    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
    }
}