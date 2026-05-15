using ExpenseTracker.Models;
using ExpenseTracker.Services;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers
{
    public class ExpensesController : Controller
    {
        // Внедрение зависимости через конструктор
        private readonly IExpenseService _expenseService;

        public ExpensesController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }


        [HttpGet]
        public IActionResult List(string? category, DateTime? from, DateTime? to)
        {
            List<Expense> expenses;

            // Фильтр по категории
            if (!string.IsNullOrEmpty(category))
            {
                expenses = _expenseService.GetByCategory(category);
                ViewBag.Category = category;
            }
            // Фильтр по дате
            else if (from.HasValue && to.HasValue)
            {
                expenses = _expenseService.GetByDateRange(from.Value, to.Value);
                ViewBag.From = from.Value.ToString("yyyy-MM-dd");
                ViewBag.To = to.Value.ToString("yyyy-MM-dd");
            }
            else
            {
                expenses = _expenseService.GetAll();
            }

            // ViewBag для статистики
            ViewBag.Total = expenses.Count;
            ViewBag.TotalAmount = expenses.Sum(e => e.Amount);
            ViewBag.WeekTotal = _expenseService.GetTotalForPeriod(
                DateTime.Now.AddDays(-7), DateTime.Now
            );

            return View(expenses);
        }


        [HttpGet]
        public IActionResult Filter(string category)
        {
            return RedirectToAction("List", new { category });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Expenses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var expense = new Expense
            {
                Title = model.Title,
                Amount = model.Amount,
                Category = model.Category,
                Date = model.Date
            };

            _expenseService.Add(expense);

            // TempData — всплывающее уведомление
            TempData["Message"] = $"Расход '{model.Title}' на сумму {model.Amount:N2} успешно добавлен!";

            return RedirectToAction("Create");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var expense = _expenseService.GetById(id);
            if (expense == null) return NotFound();

            var model = new ExpenseViewModel
            {
                Id = expense.Id,
                Title = expense.Title,
                Amount = expense.Amount,
                Category = expense.Category,
                Date = expense.Date
            };

            return View(model);
        }

        // POST: /Expenses/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var expense = new Expense
            {
                Id = model.Id,
                Title = model.Title,
                Amount = model.Amount,
                Category = model.Category,
                Date = model.Date
            };

            _expenseService.Update(expense);
            TempData["Message"] = $"Расход '{model.Title}' успешно обновлён!";

            return RedirectToAction("List");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _expenseService.Delete(id);
            TempData["Message"] = "Расход успешно удалён!";
            return RedirectToAction("List");
        }
    }
}