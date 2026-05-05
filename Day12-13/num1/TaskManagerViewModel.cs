using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace num1
{
    public class TaskManagerViewModel
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }

        public TaskManagerViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>
            {
                new TaskItem
                {
                    Title = "Пример задачи",
                    Description = "Это описание задачи",
                    CreatedDate = DateTime.Now
                }
            };

            AddTaskCommand = new RelayCommand(o => AddTask());
            EditTaskCommand = new RelayCommand(o => EditTask(), o => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(o => DeleteTask(), o => SelectedTask != null);
        }

        private void AddTask()
        {
            var editModel = new TaskEditModel
            {
                Title = string.Empty,
                Description = string.Empty,
                IsEditMode = false
            };

            var editWindow = new TaskEditWindow(editModel);

            if (editWindow.ShowDialog() == true)
            {
                var newTask = new TaskItem
                {
                    Title = editModel.Title,
                    Description = editModel.Description,
                    CreatedDate = DateTime.Now
                };

                Tasks.Add(newTask);
                SelectedTask = newTask;
            }
        }

        private void EditTask()
        {
            if (SelectedTask == null) return;

            var editModel = new TaskEditModel
            {
                Title = SelectedTask.Title,
                Description = SelectedTask.Description,
                IsEditMode = true
            };

            var editWindow = new TaskEditWindow(editModel);

            if (editWindow.ShowDialog() == true)
            {
                SelectedTask.Title = editModel.Title;
                SelectedTask.Description = editModel.Description;

                RefreshTasksList();
            }
        }

        private void RefreshTasksList()
        {
            var tempList = new ObservableCollection<TaskItem>(Tasks);

            Tasks.Clear();

            foreach (var task in tempList)
            {
                Tasks.Add(task);
            }
        }

        private void DeleteTask()
        {
            var result = MessageBox.Show($"Вы уверены, что хотите удалить задачу \"{SelectedTask.Title}\"?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes && SelectedTask != null)
            {
                Tasks.Remove(SelectedTask);
            }
        }
    }
}