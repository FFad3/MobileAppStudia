using System.Windows.Input;

namespace App.Core.Helpers
{
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly Action _Action;

        public RelayCommand(Action action)
        {
            _Action = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _Action();
        }
    }
}