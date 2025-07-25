using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TradebackParser.FilePicker;

namespace TradebackParser.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase _contentViewModel;
    
    public string FileName { get; set; }

    public MainViewModel(IFilePickerService filePickerService)
    {
        FileName = string.Empty;
        _contentViewModel = new OpenFileViewModel(filePickerService);
        (_contentViewModel as OpenFileViewModel)!.FileOpened += SwitchToItems;
    }

    private void SwitchToItems(object? sender, EventArgs e)
    {
        if (e is CustomEventArgs args) FileName = args.Data;
        ContentViewModel = new ItemsViewModel();
    }
}