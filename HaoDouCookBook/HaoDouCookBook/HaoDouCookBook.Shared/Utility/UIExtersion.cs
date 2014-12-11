using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace Shared.Utility
{
    public static class UIExtersion
    {
        public static T GetParent<T>(this FrameworkElement element) where T : FrameworkElement
        {
            if (element == null)
            {
                return null;
            }

            var parent = VisualTreeHelper.GetParent(element);

            if(parent == null)
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
}
