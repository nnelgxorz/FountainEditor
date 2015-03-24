using System;
using System.Windows.Input;

namespace FountainEditorGUI.Commands
{
    public abstract class CommandBase<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        public virtual bool CanExecute(T parameter)
        {
            return true;
        }

        public void Execute(object parameter) {
            Execute((T)parameter);
        }

        public abstract void Execute(T parameter);
    }
}
