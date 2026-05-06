using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace num1
{
    public enum TaskStatus
    {
        Ожидание,
        ВРаботе,
        Выполнено
    }

    public class TaskItem : INotifyPropertyChanged
    {
        private string _title;
        private string _description;
        private DateTime _deadline;
        private TaskStatus _status;

        public string Title
        {
            get => _title;
            set { _title = value; OnPropertyChanged(); }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public DateTime Deadline
        {
            get => _deadline;
            set { _deadline = value; OnPropertyChanged(); }
        }

        public TaskStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case TaskStatus.Ожидание: return "⏳ Ожидание";
                    case TaskStatus.ВРаботе: return "🔄 В работе";
                    case TaskStatus.Выполнено: return "✅ Выполнено";
                    default: return "Неизвестно";
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}