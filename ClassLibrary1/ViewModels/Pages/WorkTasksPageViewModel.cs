using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoList.Core.Helpers;
using ToDoList.Core.ViewModels.Base;
using ToDoList.Database.Entities;

namespace ToDoList.Core
{
    public class WorkTasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTaskViewModel> WorkTaskList { get; set; } = new ObservableCollection<WorkTaskViewModel>();
        public string NewWorkTaskTitle { get; set; }
        public string NewWorkTaskDescription { get; set; }
        private WorkTaskViewModel workTaskViewModel;

        public WorkTaskViewModel SelectedTask
        {
            get { return workTaskViewModel; }
            set
            {
                workTaskViewModel = value;
                if (value == null)
                {
                    NewWorkTaskTitle = string.Empty;
                    NewWorkTaskDescription = string.Empty;
                    OnPropertyChange(nameof(NewWorkTaskTitle));
                    OnPropertyChange(nameof(NewWorkTaskDescription));
                }
                else
                {
                    NewWorkTaskTitle = value.Title;
                    NewWorkTaskDescription = value.Description;
                    OnPropertyChange(nameof(NewWorkTaskTitle));
                    OnPropertyChange(nameof(NewWorkTaskDescription));
                }
            }
        }

        //Commands
        public ICommand AddNewTaskCommand { get; set; }

        public ICommand DeleteSelectedTasksCommand { get; set; }

        public ICommand UpdateSelectedWorkTaskCommand { get; set; }

        public WorkTasksPageViewModel()
        {
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
            UpdateSelectedWorkTaskCommand = new RelayCommand(UpdateTask);
            GetData();
        }

        private void GetData()
        {
            WorkTaskList.Clear();
            foreach (var item in DataBaseLocator.Database.WorkTasks.ToList())
            {
                WorkTaskList.Add(new WorkTaskViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    CreatedDate = item.CreatedDate
                });
            }
        }

        private void UpdateTask()
        {
            if (SelectedTask == null) return;
            int oldIndex = WorkTaskList.IndexOf(SelectedTask);
            var item = DataBaseLocator.Database.WorkTasks.FirstOrDefault(x => x.Id == SelectedTask.Id);
            if (item == null) return;

            var newTask = new WorkTask
            {
                Id = SelectedTask.Id,
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
            };
            DataBaseLocator.Database.WorkTasks.Remove(item);
            DataBaseLocator.Database.WorkTasks.Add(newTask);
            DataBaseLocator.Database.SaveChanges();
            GetData();
            SelectedTask = null;
        }

        private void AddNewTask()
        {
            var db = DataBaseLocator.Database.WorkTasks;
            db.Add(new WorkTask
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
            });
            DataBaseLocator.Database.SaveChanges();
            GetData();
            NewWorkTaskTitle = String.Empty;
            NewWorkTaskDescription = String.Empty;

            OnPropertyChange(nameof(NewWorkTaskTitle));
            OnPropertyChange(nameof(NewWorkTaskDescription));
        }

        private void DeleteSelectedTasks()
        {
            var db = DataBaseLocator.Database.WorkTasks;
            foreach (var task in WorkTaskList.Where(x => x.IsSelected).ToList())
            {
                WorkTaskList.Remove(task);
                var item = db.FirstOrDefault(x => x.Id == task.Id);
                if (item != null)
                {
                    db.Remove(item);
                }
            }
            DataBaseLocator.Database.SaveChanges();
        }
    }
}