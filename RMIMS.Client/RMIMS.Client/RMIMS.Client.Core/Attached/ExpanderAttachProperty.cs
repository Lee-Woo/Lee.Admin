using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RMIMS.Client.Core.Attached
{
    public class ExpanderAttachProperty : DependencyObject
    {
        public static void SetIconForeground(DependencyObject obj, string value)
        {
            obj.SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty =
            DependencyProperty.RegisterAttached("IconForeground", typeof(string), typeof(ExpanderAttachProperty), new PropertyMetadata(null));

        public static bool GetClose(DependencyObject obj)
        {
            return (bool)obj.GetValue(CloseProperty);
        }

        public static void SetClose(DependencyObject obj, bool value)
        {
            obj.SetValue(CloseProperty, value);
        }

        public static readonly DependencyProperty CloseProperty =
            DependencyProperty.RegisterAttached("Close", typeof(bool), typeof(ExpanderAttachProperty), new PropertyMetadata(null));

    }
}
