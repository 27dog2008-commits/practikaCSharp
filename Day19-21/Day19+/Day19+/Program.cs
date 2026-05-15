using ExpenseTracker.Data;
using ExpenseTracker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Регистрация DbContext с SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=expenses.db"));

// Регистрация сервиса через DI
builder.Services.AddScoped<IExpenseService, ExpenseService>();

var app = builder.Build();

// Авто-создание БД при старте
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "filter",
    pattern: "Expenses/Filter/{category}",
    defaults: new { controller = "Expenses", action = "Filter" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Expenses}/{action=List}/{id?}"
);

app.Run();