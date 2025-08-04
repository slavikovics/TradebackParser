using System.Collections.Generic;

namespace TradebackParser.ViewModels;

public class DownloadsViewModel : ViewModelBase
{
    private List<ItemModel> _items;
    
    public DownloadsViewModel(List<ItemModel> items)
    {
        _items = items;
    }
}