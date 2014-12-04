using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
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
    }
}
