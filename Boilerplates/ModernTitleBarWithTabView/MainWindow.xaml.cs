using Windows.Graphics;

using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Media;

namespace ModernTitleBarWithTabView;

public sealed partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();

        if (MicaController.IsSupported())
        {
            SystemBackdrop = new MicaBackdrop();
        }
        else if (DesktopAcrylicController.IsSupported())
        {
            SystemBackdrop = new DesktopAcrylicBackdrop();
        }

        if (AppWindowTitleBar.IsCustomizationSupported())
        {
            AppWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            AppWindow.TitleBar.ButtonBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonInactiveBackgroundColor = Colors.Transparent;

            AppTabView.SizeChanged += (_, _) => UpdateDragRectangles();
            RightPaddingInset.SizeChanged += (_, _) => UpdateDragRectangles();
        }
    }

    private const int DragRegionMinWidth = 50;

    private void UpdateDragRectangles()
    {
        LeftPaddingInset.MinWidth = AppWindow.TitleBar.LeftInset;
        RightPaddingInset.MinWidth = AppWindow.TitleBar.RightInset + DragRegionMinWidth;

        var dragRect = new RectInt32
        {
            X = (int)(AppTabView.ActualWidth - RightPaddingInset.ActualWidth),
            Y = 0,
            Height = (int)AppTabView.ActualHeight,
            Width = (int)(RightPaddingInset.ActualWidth - AppWindow.TitleBar.RightInset)
        };

        AppWindow.TitleBar.SetDragRectangles(new[] { dragRect });
    }
}