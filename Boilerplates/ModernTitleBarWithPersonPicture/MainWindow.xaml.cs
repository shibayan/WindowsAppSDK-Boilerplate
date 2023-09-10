using Windows.Graphics;

using Microsoft.UI;
using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;

namespace ModernTitleBarWithPersonPicture;

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
            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;

            AppTitleBar.Loaded += (_, _) => UpdateDragRectangles();
            AppTitleBar.SizeChanged += (_, _) => UpdateDragRectangles();
        }
    }

    private void UpdateDragRectangles()
    {
        LeftPaddingColumn.Width = new GridLength(AppWindow.TitleBar.LeftInset);
        RightPaddingColumn.Width = new GridLength(AppWindow.TitleBar.RightInset);

        var leftDragRect = new RectInt32
        {
            X = (int)LeftPaddingColumn.ActualWidth,
            Y = 0,
            Width = (int)(IconColumn.ActualWidth + TitleColumn.ActualWidth + LeftDragColumn.ActualWidth),
            Height = (int)AppTitleBar.ActualHeight
        };

        var rightDragRect = new RectInt32
        {
            X = (int)(AppTitleBar.ActualWidth - RightDragColumn.ActualWidth - RightPaddingColumn.ActualWidth),
            Y = 0,
            Width = (int)RightDragColumn.ActualWidth,
            Height = (int)AppTitleBar.ActualHeight
        };

        AppWindow.TitleBar.SetDragRectangles(new[] { leftDragRect, rightDragRect });
    }

    private void PersonButton_Click(object sender, RoutedEventArgs e)
    {
        FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
    }
}