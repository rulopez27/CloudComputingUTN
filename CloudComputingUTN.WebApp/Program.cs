using CloudComputingUTN.Middleware;
using CloudComputingUTN.Sqlite;
using CloudComputingUTN.WebApp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<DbContext, MuseumDbContext>();

#if DATABASE_ENGINE_SQLITE
DatabaseSetup sqliteDbSetup = new DatabaseSetup();
sqliteDbSetup.SetupInMemoryDatabase();
builder.Services.AddDbContext<MuseumDbContext>(options => options.UseSqlite(sqliteDbSetup.GetDbConnection()));
DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.SQLite);
#endif

#if DATABASE_ENGINE_MSSQL
builder.Services
    .AddDbContext<MuseumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.MSSQL);
#endif

#if DATABASE_ENGINE_MYSQL
builder.Services
    .AddDbContext<MuseumDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));
DatabaseEngine.InstanceDatabaseEngine().SetEngine(DatabaseEngines.MySQL);
#endif

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