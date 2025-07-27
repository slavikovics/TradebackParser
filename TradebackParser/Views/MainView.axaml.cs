using System.Threading.Tasks;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TradebackParser.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        StartAnimation();
    }

    public async Task StartAnimation()
    {
        var animation = (Animation)this.Resources["PhaseCompletedAnimation"];
        await animation.RunAsync(WebsiteBorder);
    }
}