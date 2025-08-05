using System.Threading.Tasks;

namespace TradebackParser.FilePicker;

public interface IFilePickerService
{
    Task<string?> PickFileAsync();
    
    Task<string?> SaveCsvFileAsync();
}