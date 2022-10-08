using App.Core.Helpers;
using App.Core.ViewModels.Base;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace App.Core
{
    public class WorkTasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();
        public string NewWorkTaskTitle { get; set; }
        public string NewWorkTaskDescription { get; set; }

        //Commands
        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommand { get; set; }

        public WorkTasksPageViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        }

        private void AddNewTask()
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                CreatedDate = DateTime.Now
            };

            WorkTaskList.Add(newTask);

            NewWorkTaskTitle = String.Empty;
            NewWorkTaskDescription = String.Empty;

            OnPropertyChange(nameof(NewWorkTaskTitle));
            OnPropertyChange(nameof(NewWorkTaskDescription));
        }

        private void DeleteSelectedTasks()
        {
            foreach (var task in WorkTaskList.Where(x => x.IsSelected))
            {
                WorkTaskList.Remove(task);
            }
        }
    }
}