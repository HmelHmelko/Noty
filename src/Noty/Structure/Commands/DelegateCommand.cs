using System;
using Noty.Structure.Commands.Base;

namespace Noty.Commands
{
    public class DelegateCommand : BaseCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this._canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => this._canExecute?.Invoke(parameter) ?? true;
        public override void Execute(object parameter) => this._execute(parameter);
    }
}
