using System.Windows;

namespace Noty.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter) => true;
        public override void Execute(object? parameter) => Application.Current.Shutdown();
    }
}
