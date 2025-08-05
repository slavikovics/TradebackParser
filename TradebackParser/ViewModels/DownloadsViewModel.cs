using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using TradebackParser.FilePicker;

namespace TradebackParser.ViewModels;

public partial class DownloadsViewModel : ViewModelBase
{
    private List<ItemModel> _items;
    
    private IFilePickerService _filePickerService;
    
    public DownloadsViewModel(List<ItemModel> items, IFilePickerService filePickerService)
    {
        _items = items;
        _filePickerService = filePickerService;
    }

    [RelayCommand]
    private void DownloadCsvFile()
    {
        CsvHelper.SaveListToCsv(_items, _filePickerService);
    }
}

