using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TradebackParser.ViewModels;

public partial class ItemsListViewModel : ViewModelBase
{
    [ObservableProperty] 
    private bool _loadingButtonEnabled = false;
    
    [ObservableProperty] 
    private bool _numericUppDownEnabled = false;
    
    [ObservableProperty] 
    private bool _openOutputFileEnabled = false;
    
    [ObservableProperty] 
    private bool _saveDataEnabled = false;

    [RelayCommand]
    public void OpenOutputFileCommand()
    {
    }
}