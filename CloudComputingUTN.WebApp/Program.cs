using CloudComputingUTN.Middleware;
using CloudComputingUTN.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DbContext, MuseumDbContext>();

///TODO: Si vas a usar MySQL descomenta la siguiente línea de código
//builder.Services
//    .AddDbContext<MuseumDbContext>(options => options.UseMySql(Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));

///TODO: Si vas a usar Microsoft SQL Server, descomenta la siguiente línea de código
//builder.Services
//    .AddDbContext<MuseumDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MSSQL")));

///TODO: Si vas a usar SQLite, descomenta las siguientes líneas de código
DatabaseSetup sqliteDbSetup = new DatabaseSetup();
sqliteDbSetup.SetupInMemoryDatabase();
builder.Services.AddDbContext<MuseumDbContext>(options => options.UseSqlite(sqliteDbSetup.GetDbConnection()));

builder.Services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
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

app.Run();