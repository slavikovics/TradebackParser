using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TradebackParser.ViewModels;

namespace TradebackParser.Views;

public partial class ItemsListView : UserControl
{
    public ItemsListView()
    {
        InitializeComponent();
        DataContext = new ItemsListViewModel();
    }
}