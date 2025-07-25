using Avalonia.Controls;
using TradebackParser.FilePicker;
using TradebackParser.ViewModels;

namespace TradebackParser.Views;

public partial class MainWindow : Window
{
    public MainWindow(TopLevelService topLevelService)
    {
        InitializeComponent();
        topLevelService.MainWindow = this;
    }
}