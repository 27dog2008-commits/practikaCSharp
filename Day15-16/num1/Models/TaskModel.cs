using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace num1.Models
{
    public enum MyTaskStatus
    {
        Ожидание,
        ВРаботе,
        Выполнено
    }

    public class TaskModel : INotifyPropertyChanged
    {
        private int _id;
        private string _title;
        private string _description;
        private DateTime _deadline;
        private DateTime _createdDate;
        private MyTaskStatus _status;
        private int _categoryId;
        private TaskCategoryModel _category;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

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

        public DateTime CreatedDate
        {
            get => _createdDate;
            set { _createdDate = value; OnPropertyChanged(); }
        }

        public MyTaskStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(StatusText));
            }
        }

        public int CategoryId
        {
            get => _categoryId;
            set { _categoryId = value; OnPropertyChanged(); }
        }

        public TaskCategoryModel Category
        {
            get => _category;
            set { _category = value; OnPropertyChanged(); }
        }

        public string StatusText
        {
            get
            {
                switch (Status)
                {
                    case MyTaskStatus.Ожидание: return "⏳ Ожидание";
                    case MyTaskStatus.ВРаботе: return "🔄 В работе";
                    case MyTaskStatus.Выполнено: return "✅ Выполнено";
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