using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TradebackParser.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _contentViewModel;

    public MainViewModel()
    {
        _contentViewModel = new OpenFileViewModel();
    }
}