using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TradebackParser.ViewModels;

public partial class OpenFileViewModel : ViewModelBase
{
    public event EventHandler? FileOpened;
    
    [ObservableProperty] private string _fileName;

    public OpenFileViewModel()
    {
        _fileName = string.Empty;
    }

    [RelayCommand]
    private void OpenFile()
    {
        FileOpened?.Invoke(this, new CustomEventArgs(FileName));
    }
}