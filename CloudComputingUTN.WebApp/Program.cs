using CloudComputingUTN.Middleware;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.WebApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddDbContext<MuseumDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));

            builder.Services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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
