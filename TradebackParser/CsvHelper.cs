using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using TradebackParser.FilePicker;

namespace TradebackParser;

public class CsvHelper
{
    public static async void SaveListToCsv<T>(List<T> items, IFilePickerService filePickerService)
    {
        try
        {
            var file = await filePickerService.SaveCsvFileAsync();

            if (file != null)
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                await using var writer = new StreamWriter(File.OpenWrite(file));
                
                var headers = string.Join(",", properties.Select(p => $"\"{p.Name}\""));
                await writer.WriteLineAsync(headers);
                
                foreach (var item in items)
                {
                    var values = properties.Select(p =>
                    {
                        var value = p.GetValue(item)?.ToString() ?? "";
                        return $"\"{value.Replace("\"", "\"\"")}\"";
                    });
                    await writer.WriteLineAsync(string.Join(",", values));
                }
                
                await writer.FlushAsync();
            }
        }
        catch
        {
        }
    }
}