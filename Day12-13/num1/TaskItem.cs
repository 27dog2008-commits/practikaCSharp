using System;

namespace num1
{
    public class TaskItem
    {
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }

        public override string ToString() => $"{CreatedDate:HH:mm} - {Title}";
    }
}