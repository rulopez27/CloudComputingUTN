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
