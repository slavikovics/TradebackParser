using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TradebackParser.ViewModels;

public partial class ItemsViewModel : ViewModelBase
{
    public int ItemCount => Items?.Count ?? 0;
    
    private ObservableCollection<ItemModel> _items;
    public ObservableCollection<ItemModel> Items
    {
        get => _items;
        set => _items = value;
    }
}

public class ItemModel
{
    public string Name { get; set; }
    public decimal Price1 { get; set; }
    public decimal Price2 { get; set; }
}