using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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

    public ItemsViewModel(string fileName)
    {
        string content = ReadFile(fileName);
        Parser parser = new Parser(content);
        _items = new ObservableCollection<ItemModel>();

        foreach (var item in parser.Parse())
        {
            _items.Add(item);
        }
    }

    private string ReadFile(string fileName)
    {
        return File.ReadAllText(fileName);
    }
}