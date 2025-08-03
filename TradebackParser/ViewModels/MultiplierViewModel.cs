using System.Collections.Generic;
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
    
    public List<ItemModel> Items { get; set; }

    public MultiplierViewModel(List<ItemModel> items)
    {
        Items = items;
    }
}