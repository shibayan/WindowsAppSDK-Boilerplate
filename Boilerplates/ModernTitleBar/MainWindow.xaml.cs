using Windows.Graphics;

using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace ModernTitleBar;

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

            AppTitleBar.Loaded += (_, _) => UpdateDragRectangles();
            AppTitleBar.SizeChanged += (_, _) => UpdateDragRectangles();
        }
    }

    private void UpdateDragRectangles()
    {
        LeftPaddingColumn.Width = new GridLength(AppWindow.TitleBar.LeftInset);
        RightPaddingColumn.Width = new GridLength(AppWindow.TitleBar.RightInset);

        var dragRect = new RectInt32
        {
            X = (int)LeftPaddingColumn.ActualWidth,
            Y = 0,
            Height = (int)AppTitleBar.ActualHeight,
            Width = (int)(AppTitleBar.ActualWidth - LeftPaddingColumn.ActualWidth - RightPaddingColumn.ActualWidth)
        };

        AppWindow.TitleBar.SetDragRectangles(new[] { dragRect });
    }
}