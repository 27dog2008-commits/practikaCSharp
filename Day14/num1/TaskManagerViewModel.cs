using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace num1
{
    public class TaskManagerViewModel
    {
        public string AddTaskShortcut => "Ctrl+N";
        public string EditTaskShortcut => "Ctrl+E";
        public string DeleteTaskShortcut => "Ctrl+D";
        public string ChangeStatusShortcut => "Ctrl+S";

        public ObservableCollection<TaskItem> Tasks { get; set; }
        private TaskItem _selectedTask;
        public TaskItem SelectedTask
        {
            get => _selectedTask;
            set
            {
                _selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        private string _statusFilter;
        public string StatusFilter
        {
            get => _statusFilter;
            set
            {
                _statusFilter = value;
                OnPropertyChanged(nameof(StatusFilter));
                FilterTasks();
            }
        }

        public ObservableCollection<string> FilterOptions { get; set; }
        private ObservableCollection<TaskItem> _allTasks;

        public ICommand AddTaskCommand { get; }
        public ICommand EditTaskCommand { get; }
        public ICommand DeleteTaskCommand { get; }
        public ICommand ChangeStatusCommand { get; }

        public TaskManagerViewModel()
        {
            _allTasks = new ObservableCollection<TaskItem>();
            Tasks = new ObservableCollection<TaskItem>();

            FilterOptions = new ObservableCollection<string> { "Все", "Ожидание", "В работе", "Выполнено" };
            StatusFilter = "Все";

            // Примеры задач
            _allTasks.Add(new TaskItem
            {
                Title = "Изучить WPF",
                Description = "Прочитать документацию",
                Deadline = DateTime.Now.AddDays(2),
                Status = TaskStatus.ВРаботе
            });

            _allTasks.Add(new TaskItem
            {
                Title = "Сделать проект",
                Description = "Написать код",
                Deadline = DateTime.Now.AddDays(5),
                Status = TaskStatus.Ожидание
            });

            FilterTasks();

            AddTaskCommand = new RelayCommand(o => AddTask());
            EditTaskCommand = new RelayCommand(o => EditTask(), o => SelectedTask != null);
            DeleteTaskCommand = new RelayCommand(o => DeleteTask(), o => SelectedTask != null);
            ChangeStatusCommand = new RelayCommand(o => ChangeStatus(), o => SelectedTask != null);
        }

        private void FilterTasks()
        {
            Tasks.Clear();

            if (StatusFilter == "Все")
            {
                foreach (var task in _allTasks)
                    Tasks.Add(task);
            }
            else
            {
                TaskStatus status;
                switch (StatusFilter)
                {
                    case "Ожидание": status = TaskStatus.Ожидание; break;
                    case "В работе": status = TaskStatus.ВРаботе; break;
                    default: status = TaskStatus.Выполнено; break;
                }

                foreach (var task in _allTasks)
                {
                    if (task.Status == status)
                        Tasks.Add(task);
                }
            }
        }

        private void AddTask()
        {
            var window = new TaskEditWindow();
            window.Owner = App.Current.MainWindow;
            window.Title = "Создание задачи";

            var result = window.ShowDialog();
            if (result == true)
            {
                var newTask = new TaskItem
                {
                    Title = window.TaskTitle,
                    Description = window.TaskDescription,
                    Deadline = window.TaskDeadline,
                    Status = window.TaskStatus
                };
                _allTasks.Add(newTask);
                FilterTasks();
            }
        }

        private void EditTask()
        {
            if (SelectedTask == null) return;

            var window = new TaskEditWindow(
                SelectedTask.Title,
                SelectedTask.Description,
                SelectedTask.Deadline,
                SelectedTask.Status
            );
            window.Owner = App.Current.MainWindow;
            window.Title = "Редактирование задачи";

            var result = window.ShowDialog();
            if (result == true)
            {
                SelectedTask.Title = window.TaskTitle;
                SelectedTask.Description = window.TaskDescription;
                SelectedTask.Deadline = window.TaskDeadline;
                SelectedTask.Status = window.TaskStatus;
                FilterTasks();
            }
        }

        private void DeleteTask()
        {
            if (SelectedTask != null)
            {
                if (MessageBox.Show($"Удалить задачу \"{SelectedTask.Title}\"?",
                    "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _allTasks.Remove(SelectedTask);
                    FilterTasks();
                }
            }
        }

        private void ChangeStatus()
        {
            if (SelectedTask == null) return;

            switch (SelectedTask.Status)
            {
                case TaskStatus.Ожидание:
                    SelectedTask.Status = TaskStatus.ВРаботе;
                    break;
                case TaskStatus.ВРаботе:
                    SelectedTask.Status = TaskStatus.Выполнено;
                    break;
                case TaskStatus.Выполнено:
                    SelectedTask.Status = TaskStatus.Ожидание;
                    break;
            }

            FilterTasks();
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
    }
}