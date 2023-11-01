using System.Windows.Input;
using System;

namespace Noty.Structure.Commands.Base
{
    public abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);
    }
}