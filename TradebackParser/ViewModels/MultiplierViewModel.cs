using CommunityToolkit.Mvvm.ComponentModel;

namespace TradebackParser.ViewModels;

public partial class MultiplierViewModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isFirstCountRuleActive = true;
    
    [ObservableProperty]
    private bool _isSecondCountRuleActive = true;
    
    [ObservableProperty]
    private bool _isPhraseFilterActive = true;
}