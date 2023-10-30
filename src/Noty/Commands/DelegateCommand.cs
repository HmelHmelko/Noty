using System;
using System.Windows.Input;

namespace Noty.Commands
{
    public class DelegateCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;
        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute(parameter);
        public void Execute(object parameter) => this.execute(parameter);

        public event EventHandler? CanExecuteChanged;
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
