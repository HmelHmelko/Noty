using Noty.Structure.Commands.Base;
using Noty.Views.Windows;
using System.Windows;

namespace Noty.Commands
{
    public class ChangeWindowStateCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter) => true;
        public override void Execute(object? parameter)
        {
            var window = (MainWindow) parameter;
            window.WindowState = window.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }
    }
}
