using System;
using System.Windows.Input;

namespace FountainEditorGUI.Commands {
    public sealed class RelayCommand<T> : ICommand {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<T> execute) {
            this.execute = execute;
            this.canExecute = e => true;
        }

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute) {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            return canExecute((T)parameter);
        }

        public void Execute(object parameter) {
            execute((T)parameter);
        }
    }
}
