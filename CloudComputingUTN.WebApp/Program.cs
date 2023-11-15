using CloudComputingUTN.Middleware;
using CloudComputingUTN.Sqlite;
using CloudComputingUTN.WebApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DbContext, MuseumDbContext>();

///TODO: Si vas a usar MySQL descomenta la siguiente línea de código
//builder.Services
//    .AddDbContext<MuseumDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));
//DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.MySQL);
///END

///TODO: Si vas a usar Microsoft SQL Server, descomenta la siguiente línea de código
//builder.Services
//    .AddDbContext<MuseumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
//DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.MSSQL);
///END

///TODO: Si vas a usar SQLite, descomenta las siguientes líneas de código
DatabaseSetup sqliteDbSetup = new DatabaseSetup();
sqliteDbSetup.SetupInMemoryDatabase();
builder.Services.AddDbContext<MuseumDbContext>(options => options.UseSqlite(sqliteDbSetup.GetDbConnection()));
DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.SQLite);
///END

builder.Services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    DatabaseEngine.InstanceDatabaseEngine().SetEnvironment(AppEnvironments.Development);
}
else
{
    DatabaseEngine.InstanceDatabaseEngine().SetEnvironment(AppEnvironments.Production);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();