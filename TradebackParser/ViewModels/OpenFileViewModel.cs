using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TradebackParser.FilePicker;

namespace TradebackParser.ViewModels;

public partial class OpenFileViewModel : ViewModelBase
{
    public event EventHandler? FileOpened;
    
    private readonly IFilePickerService _filePickerService;
    
    [ObservableProperty] private string _fileName;

    public OpenFileViewModel(IFilePickerService filePickerService)
    {
        _fileName = string.Empty;
        _filePickerService = filePickerService;
    }

    [RelayCommand]
    private async Task PickFile()
    {
        string? fileName = await _filePickerService.PickFileAsync();
        if (!string.IsNullOrEmpty(fileName))
        {
            FileName = fileName;
        }
    }

    [RelayCommand]
    private void OpenFile()
    {
        FileOpened?.Invoke(this, new CustomEventArgs(FileName));
    }
}