using System;
using Noty.Structure.Commands.Base;

namespace Noty.Commands
{
    public class DelegateCommand : BaseCommand
    {
        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => this.execute(parameter);
    }
}
