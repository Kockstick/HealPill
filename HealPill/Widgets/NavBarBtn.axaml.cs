using System;
using System.Runtime.InteropServices.JavaScript;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace HealPill.Widgets;

public partial class NavBarBtn : Border
{
    public string Content
    {
        get => Name_Label.Content.ToString();
        set => Name_Label.Content = value;
    }

    private bool _enabled;

    public bool enabled
    {
        get => _enabled;
        set
        {
            _enabled = value;

            if (enabled)
            {
                Background = new SolidColorBrush((Color)App.Current.Resources["Accent"]);
                Name_Label.Foreground = new SolidColorBrush(Colors.White);
            }
            else
            {
                Background = new SolidColorBrush(Colors.Transparent);
                Name_Label.Foreground = new SolidColorBrush(Colors.Black);
            }
        }
    }
    
    public NavBarBtn()
    {
        InitializeComponent();
    }

    private void OnMouse_Enter(object? sender, PointerEventArgs e)
    {
        if(enabled)
            return;
        Background = new SolidColorBrush(Colors.LightGray);
    }

    private void OnMouse_Exit(object? sender, PointerEventArgs e)
    {
        if(enabled)
            return;
        Background = new SolidColorBrush(Colors.Transparent);
    }


    private void OnMouse_Click(object? sender, PointerPressedEventArgs e)
    {
        NavBar.clickedBtn = this;
    }
}