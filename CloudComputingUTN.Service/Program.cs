using CloudComputingUTN.DataAccessLayer;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.Service;
using CloudComputingUTN.Sqlite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbContext, MuseumDbContext>();

#if DATABASE_ENGINE_MYSQL
builder.Services
    .AddDbContext<MuseumDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), ServerVersion.Parse("8.0.31-mysql")));
#endif

#if DATABASE_ENGINE_MSSQL
builder.Services
    .AddDbContext<MuseumDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));
#endif

#if DATABASE_ENGINE_SQLITE
DatabaseSetup sqliteDbSetup = new DatabaseSetup();
sqliteDbSetup.SetupInMemoryDatabase();
builder.Services.AddDbContext<MuseumDbContext>(options => options.UseSqlite(sqliteDbSetup.GetDbConnection()));
#endif

builder.Services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
