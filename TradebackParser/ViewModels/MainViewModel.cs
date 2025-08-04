using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TradebackParser.FilePicker;

namespace TradebackParser.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ViewModelBase _contentViewModel;

    [ObservableProperty] 
    private SolidColorBrush _websiteBorderColor = new (Color.FromArgb(255, 92, 91, 91));
    
    [ObservableProperty] 
    private SolidColorBrush _gridBorderColor = new (Color.FromArgb(255, 92, 91, 91));
    
    [ObservableProperty] 
    private SolidColorBrush _multiplierBorderColor = new (Color.FromArgb(255, 92, 91, 91));
    
    [ObservableProperty] 
    private SolidColorBrush _downloadBorderColor = new (Color.FromArgb(255, 92, 91, 91));
    
    public string FileName { get; set; }

    public event Action? GridPhase;

    public event Action? MultiplierPhase;
    
    public event Action? DownloadPhase;

    public MainViewModel(IFilePickerService filePickerService)
    {
        FileName = string.Empty;
        _contentViewModel = new OpenFileViewModel(filePickerService);
        (_contentViewModel as OpenFileViewModel)!.FileOpened += SwitchToItems;
    }

    private void SwitchToItems(object? sender, EventArgs e)
    {
        GridPhase?.Invoke();
        WebsiteBorderColor = new SolidColorBrush(Colors.DarkGreen);
        if (e is CustomEventArgs args) FileName = (string) args.Data;
        ContentViewModel = new ItemsViewModel(FileName);
        (ContentViewModel as ItemsViewModel)!.ItemsPreviewFinished += SwitchToMultipliers;
    }

    private void SwitchToMultipliers(object? sender, EventArgs e)
    {
        MultiplierPhase?.Invoke();
        GridBorderColor = new SolidColorBrush(Colors.DarkGreen);
        ContentViewModel = new MultiplierViewModel(((ObservableCollection<ItemModel>)(e as CustomEventArgs)!.Data).ToList());
        (ContentViewModel as MultiplierViewModel)!.SwitchToDownloadsEvent += SwitchToDownload;
    }

    private void SwitchToDownload(object? sender, EventArgs e)
    {
        DownloadPhase?.Invoke();
        MultiplierBorderColor = new SolidColorBrush(Colors.DarkGreen);
        ContentViewModel = new DownloadsViewModel((List<ItemModel>)(e as CustomEventArgs)!.Data);
    }
}