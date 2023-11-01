using Noty.AppSettings;
using Noty.Structure.Commands.Base;
using System.Windows;

namespace Noty.Commands
{
    public class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object? parameter) => true;
        public override void Execute(object? parameter)
        {
            Settings.Default.Save();
            Application.Current.Shutdown();
        }
    }
}
