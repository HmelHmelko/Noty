using Noty.Views.Windows;
using System.Windows;

namespace Noty.Commands
{
    public class MinimizeWindowCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter) => true;
        public override void Execute(object? parameter)
        {
            var window = (MainWindow) parameter;
            window.ShowInTaskbar = true;
            window.WindowState = WindowState.Minimized;
        }
    }
}
