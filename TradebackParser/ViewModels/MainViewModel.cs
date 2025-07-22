using CommunityToolkit.Mvvm.ComponentModel;

namespace TradebackParser.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private bool _loadingButtonEnabled = false;
    
    [ObservableProperty] private bool _numericUppDownEnabled = false;
    
    [ObservableProperty] private bool _openOutputFileEnabled = false;
    
    [ObservableProperty] private bool _saveDataEnabled = false;
}