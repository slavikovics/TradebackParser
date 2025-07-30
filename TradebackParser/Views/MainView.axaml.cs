using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.DependencyInjection;
using TradebackParser.ViewModels;

namespace TradebackParser.Views;

public partial class MainView : UserControl
{
    private CancellationTokenSource? _cancellationTokenSourceOne; 
    
    private CancellationTokenSource? _cancellationTokenSourceTwo;
    
    private CancellationTokenSource? _cancellationTokenSourceThree;
    
    private CancellationTokenSource? _cancellationTokenSourceFour;
    
    private MainViewModel? _mainViewModel;
    
    public MainView()
    {
        InitializeComponent();
        var serviceProvider = (Application.Current as App)?.ServiceProvider;
        _mainViewModel = serviceProvider?.GetRequiredService<MainViewModel>();
        FirstPhase();
    }

    private void FirstPhase()
    {
        _cancellationTokenSourceOne = new CancellationTokenSource();
        StartAnimation(WebsiteBorder, _cancellationTokenSourceOne, "CurrentPhaseAnimation");
        _mainViewModel.GridPhase += SecondPhase;
    }

    private void SecondPhase()
    {
        StopAnimation(_cancellationTokenSourceOne);
        _cancellationTokenSourceOne = new CancellationTokenSource();
        _cancellationTokenSourceTwo = new CancellationTokenSource();
        StartAnimation(WebsiteBorder, _cancellationTokenSourceOne, "PhaseCompletedAnimation");
        StartAnimation(GridBorder, _cancellationTokenSourceTwo, "CurrentPhaseAnimation");
        _mainViewModel.MultiplierPhase += ThirdPhase;
    }

    private void ThirdPhase()
    {
        StopAnimation(_cancellationTokenSourceTwo);
        _cancellationTokenSourceTwo = new CancellationTokenSource();
        _cancellationTokenSourceThree = new CancellationTokenSource();
        StartAnimation(GridBorder, _cancellationTokenSourceTwo, "PhaseCompletedAnimation");
        StartAnimation(MultiplierBorder, _cancellationTokenSourceThree, "CurrentPhaseAnimation");
        _mainViewModel.DownloadPhase += FourthPhase;
    }

    private void FourthPhase()
    {
        StopAnimation(_cancellationTokenSourceThree);
        _cancellationTokenSourceThree = new CancellationTokenSource();
        _cancellationTokenSourceFour = new CancellationTokenSource();
        StartAnimation(MultiplierBorder, _cancellationTokenSourceThree, "PhaseCompletedAnimation");
        StartAnimation(DownloadBorder, _cancellationTokenSourceFour, "CurrentPhaseAnimation");
    }
    
    private async Task StartAnimation(Border animationTarget, CancellationTokenSource cancellationTokenSource, string animationName)
    {
        Animation? animation = (Animation)Resources[animationName]!;
        try
        {
            await animation.RunAsync(animationTarget, cancellationToken: cancellationTokenSource.Token);
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void StopAnimation(CancellationTokenSource cancellationTokenSource)
    {
        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
    }
}