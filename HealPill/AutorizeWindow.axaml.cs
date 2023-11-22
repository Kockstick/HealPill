using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using HealPill.Database;

namespace HealPill;

public partial class AutorizeWindow : Window
{
    public AutorizeWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if(!DB.GetWorkerByLogin(Login_TextBox.Text, out Worker worker))
            return;
        if(worker.Password != Password_TextBox.Text)
            return;

        DB.currentWorker = worker;
        new MainWindow().Show();
        Close();
    }
}