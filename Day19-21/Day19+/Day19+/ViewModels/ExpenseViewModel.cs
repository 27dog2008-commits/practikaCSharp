using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.ViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле 'Название' обязательно")]
        [StringLength(200, ErrorMessage = "Название не должно превышать 200 символов")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Сумма' обязательно")]
        [Range(0.01, 1000000, ErrorMessage = "Сумма должна быть от 0.01 до 1 000 000")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Поле 'Дата' обязательно")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Поле 'Категория' обязательно")]
        public string Category { get; set; } = string.Empty;
    }
}