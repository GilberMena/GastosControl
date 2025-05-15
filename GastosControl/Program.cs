using GastosControl.Application.Interfaces;
using GastosControl.Application.Services;
using GastosControl.Domain.Interfaces;
using GastosControl.Infrastructure.Persistence;
using GastosControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GastosControl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IMonetaryFundRepository, MonetaryFundRepository>();
            builder.Services.AddScoped<IMonetaryFundService, MonetaryFundService>();

            builder.Services.AddScoped<IUserBudgetRepository, UserBudgetRepository>();
            builder.Services.AddScoped<IUserBudgetService, UserBudgetService>();

            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IExpenseQueries, ExpenseRepository>();

            builder.Services.AddScoped<IDepositRepository, DepositRepository>();
            builder.Services.AddScoped<IDepositService, DepositService>();

            builder.Services.AddScoped<IMovementRepository, MovementRepository>();
            builder.Services.AddScoped<IMovementService, MovementService>();


            builder.Services.AddSession();

            var app = builder.Build();

            app.UseSession();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
