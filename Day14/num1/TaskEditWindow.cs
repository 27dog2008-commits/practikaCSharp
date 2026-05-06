using System;
using System.Windows;
using System.Windows.Controls;

namespace num1
{
    public partial class TaskEditWindow : Window
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }
        public DateTime TaskDeadline { get; set; }
        public TaskStatus TaskStatus { get; set; }

        public TaskEditWindow(string title = "", string description = "",
                              DateTime? deadline = null, TaskStatus status = TaskStatus.Ожидание)
        {
            InitializeComponent();

            TitleBox.Text = title;
            DescriptionBox.Text = description;
            DeadlinePicker.SelectedDate = deadline ?? DateTime.Now.AddDays(3);

            foreach (ComboBoxItem item in StatusBox.Items)
            {
                if (item.Tag.ToString() == status.ToString())
                {
                    StatusBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Введите название задачи!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            TaskTitle = TitleBox.Text.Trim();
            TaskDescription = DescriptionBox.Text.Trim();
            TaskDeadline = DeadlinePicker.SelectedDate ?? DateTime.Now;

            string statusTag = ((ComboBoxItem)StatusBox.SelectedItem).Tag.ToString();
            switch (statusTag)
            {
                case "Ожидание": TaskStatus = TaskStatus.Ожидание; break;
                case "ВРаботе": TaskStatus = TaskStatus.ВРаботе; break;
                case "Выполнено": TaskStatus = TaskStatus.Выполнено; break;
            }

            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}