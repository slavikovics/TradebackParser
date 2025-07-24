using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TradebackParser.ViewModels;

namespace TradebackParser.Views;

public partial class ItemsView : UserControl
{
    public ItemsView()
    {
        InitializeComponent();
        DataContext = new ItemsViewModel();
    }
}