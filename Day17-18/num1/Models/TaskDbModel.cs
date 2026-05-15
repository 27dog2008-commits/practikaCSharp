using System;

namespace num1.Models
{
    public enum TaskDbStatus
    {
        Ожидание,
        ВРаботе,
        Выполнено
    }

    public class TaskDbModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CreatedDate { get; set; }
        public TaskDbStatus Status { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public TaskCategoryDbModel Category { get; set; }

        public TaskDbModel()
        {
            Title = "";
            Description = "";
            Category = new TaskCategoryDbModel();
        }
    }

    public class TaskCategoryDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public TaskCategoryDbModel()
        {
            Name = "";
            Color = "#FF2196F3";
        }
    }

    public class UserDbModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }

        public UserDbModel()
        {
            Username = "";
            PasswordHash = "";
        }
    }
}