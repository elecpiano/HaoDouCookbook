using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

public static class Extersion
{
    /// <summary>
    /// 显示context menu, date picker 等附加在FrameworkElement上的Flayout
    /// </summary>
    /// <param name="obj"></param>
    public static void ShowFlayout(this object obj)
    {
        FrameworkElement element = obj as FrameworkElement;
        if (element == null)
        {
            return;
        }

        element.ShowFlayout();
    }
}


public static class UIExtersion
{
    public static T GetParent<T>(this FrameworkElement element) where T : FrameworkElement
    {
        if (element == null)
        {
            return null;
        }

        var parent = VisualTreeHelper.GetParent(element);

        if (parent == null)
        {
            return null;
        }

        while (parent != null)
        {
            if (parent is T)
            {
                return parent as T;
            }
            else
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
        }

        return null;
    }

    /// <summary>
    /// 显示context menu, date picker 等附加在FrameworkElement上的Flayout
    /// </summary>
    /// <param name="obj"></param>
    public static void ShowFlayout(this FrameworkElement element)
    {
        FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(element);
        if (flyoutBase != null)
        {
            flyoutBase.ShowAt(element);
        }
    }


    /// <summary>
    /// Windows Phone 8.1 上ChangeView。
    /// 方案参照：
    /// https://social.msdn.microsoft.com/Forums/windowsapps/en-US/c3eac347-fe08-41bd-98cb-d97b6f260873/double-tap-to-zoom-image-windows-81-scrollviewerchangeview?forum=winappswithcsharp
    /// 
    /// </summary>
    /// <param name="scrollViewer"></param>
    /// <param name="horizaontal"></param>
    /// <param name="vertical"></param>
    /// <param name="zoom"></param>
    public static void ChangeViewExtersion(this ScrollViewer scrollViewer, double? horizaontal, double? vertical, float? zoom)
    {
        var period = TimeSpan.FromMilliseconds(0);
        Windows.System.Threading.ThreadPoolTimer.CreateTimer(async (source) =>
        {
            await scrollViewer.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                scrollViewer.ChangeView(horizaontal, vertical, zoom, true);
            });
        }, period);
    }
}
