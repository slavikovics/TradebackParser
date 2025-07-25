using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace TradebackParser.FilePicker;

public class FilePickerService : IFilePickerService
{
    private readonly TopLevel? _topLevel;

    public FilePickerService(TopLevel topLevel)
    {
        _topLevel = topLevel;
    }
    
    public async Task<string?> PickFileAsync()
    {
        if (_topLevel == null) return null;
        
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

        var files = await _topLevel.StorageProvider.OpenFilePickerAsync(options);
        return files?.Count > 0 ? files[0].TryGetLocalPath() : null;
    }
}