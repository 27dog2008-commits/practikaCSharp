using ExpenseTracker.Data;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _context;

        public ExpenseService(AppDbContext context)
        {
            _context = context;
        }

        // Все расходы
        public List<Expense> GetAll()
        {
            return _context.Expenses
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        // Фильтр по категории
        public List<Expense> GetByCategory(string category)
        {
            return _context.Expenses
                .Where(e => e.Category == category)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        // Фильтр по диапазону дат
        public List<Expense> GetByDateRange(DateTime from, DateTime to)
        {
            return _context.Expenses
                .Where(e => e.Date.Date >= from.Date && e.Date.Date <= to.Date)
                .OrderByDescending(e => e.Date)
                .ToList();
        }

        // Получить по ID
        public Expense? GetById(int id)
        {
            return _context.Expenses.FirstOrDefault(e => e.Id == id);
        }

        // Добавить
        public void Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        // Обновить
        public void Update(Expense expense)
        {
            _context.Expenses.Update(expense);
            _context.SaveChanges();
        }

        // Удалить
        public void Delete(int id)
        {
            var expense = _context.Expenses.Find(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
        }

        // Сумма за период
        public decimal GetTotalForPeriod(DateTime from, DateTime to)
        {
            var total = _context.Expenses
                .Where(e => e.Date.Date >= from.Date && e.Date.Date <= to.Date)
                .Select(e => (double?)e.Amount)
                .Sum() ?? 0;

            return (decimal)total;
        }
    }
}