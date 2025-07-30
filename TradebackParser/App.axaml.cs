using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using TradebackParser.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using TradebackParser.FilePicker;
using TradebackParser.Views;

namespace TradebackParser;

public partial class App : Application
{
    public IServiceProvider? ServiceProvider;
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow.DataContext = ServiceProvider.GetRequiredService<MainViewModel>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = ServiceProvider.GetRequiredService<MainViewModel>()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<TopLevelService>();
        services.AddTransient<MainWindow>();
        
        // Register ViewModels
        services.AddSingleton<MainViewModel>();
        services.AddTransient<OpenFileViewModel>();
        services.AddTransient<ItemsViewModel>();
        
        // Register services with factory that resolves TopLevel
        services.AddTransient<IFilePickerService, FilePickerService>();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}