using CloudComputingUTN.Middleware;
using CloudComputingUTN.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CloudComputingUTN.WebApp
{
    public class Startup
    {
        private IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext,MuseumDbContext>();

            ///TODO: Si vas a usar MySQL descomenta la siguiente línea de código
            //services
            //    .AddDbContext<MuseumDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));

            ///TODO: Si vas a usar Microsoft SQL Server, descomenta la siguiente línea de código
            //services
            //    .AddDbContext<MuseumDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MSSQL")));

            ///TODO: Si vas a usar SQLite, descomenta las siguientes líneas de código
            DatabaseSetup sqliteDbSetup = new DatabaseSetup();
            sqliteDbSetup.SetupInMemoryDatabase();
            services.AddDbContext<MuseumDbContext>(options => options.UseSqlite(sqliteDbSetup.GetDbConnection()));

            services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();
            services.AddHttpContextAccessor();

            // Add services to the container.
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
