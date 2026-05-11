using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace num1.Models
{
    public class UserModel : INotifyPropertyChanged
    {
        private int _id;
        private string _username;
        private string _passwordHash;
        private DateTime _createdDate;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
        }

        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }

        public string PasswordHash
        {
            get => _passwordHash;
            set { _passwordHash = value; OnPropertyChanged(); }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set { _createdDate = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}