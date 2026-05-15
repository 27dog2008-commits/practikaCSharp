using ExpenseTracker.Models;

namespace ExpenseTracker.Services
{
    public interface IExpenseService
    {
        List<Expense> GetAll();
        List<Expense> GetByCategory(string category);
        List<Expense> GetByDateRange(DateTime from, DateTime to);
        Expense? GetById(int id);
        void Add(Expense expense);
        void Update(Expense expense);
        void Delete(int id);
        decimal GetTotalForPeriod(DateTime from, DateTime to);
    }
}