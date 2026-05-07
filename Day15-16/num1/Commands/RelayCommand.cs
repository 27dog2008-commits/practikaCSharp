using System;
using System.Windows.Input;

namespace num1.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, System.Threading.Tasks.Task> _asyncExecute;
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Func<object, System.Threading.Tasks.Task> execute, Predicate<object> canExecute = null)
        {
            _asyncExecute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public async void Execute(object parameter)
        {
            if (_asyncExecute != null)
                await _asyncExecute(parameter);
            else
                _execute?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}