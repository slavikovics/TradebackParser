using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace TradebackParser.FilePicker;

public class FilePickerService : IFilePickerService
{
    private readonly TopLevelService _topLevelService;

    public FilePickerService(TopLevelService topLevelService)
    {
        _topLevelService = topLevelService;
    }
    
    public async Task<string?> PickFileAsync()
    {
        var htmlFilter = new FilePickerFileType("HTML Files")
        {
            Patterns = ["*.html", "*.htm"],
            AppleUniformTypeIdentifiers = ["public.html"],
            MimeTypes = ["text/html"]
        };

        var options = new FilePickerOpenOptions
        {
            Title = "Select HTML File",
            FileTypeFilter = [htmlFilter],
            AllowMultiple = false
        };

        var files = await _topLevelService.MainWindow.StorageProvider.OpenFilePickerAsync(options);
        return files?.Count > 0 ? files[0].TryGetLocalPath() : null;
    }
}