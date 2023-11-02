using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Controls;

namespace Noty.Structure.Behaviors
{
    public class WindowTopBorderBehavior : Behavior<UIElement>
    {
        private Window? window;
        protected override void OnAttached()
        {
            window = AssociatedObject as Window ?? AssociatedObject.FindLogicalAncestor<Window>();
            AssociatedObject.MouseLeftButtonDown += OnMouseDown;
        }
        protected override void OnDetaching()
        {
            AssociatedObject.MouseLeftButtonDown -= OnMouseDown;
            window = null;
        }
        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch(e.ClickCount)
            {
                case 1:
                    DragMove();
                    break;
                default:
                    Maximize();
                    break;
            }
        }

        private void DragMove()
        {
            if (!(AssociatedObject.FindVisualAncestor<Window>() is Window window)) return;

            window.DragMove();
        }

        private void Maximize()
        {
            if (!(AssociatedObject.FindVisualAncestor<Window>() is Window window)) return;

            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
