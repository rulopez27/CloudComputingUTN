using CloudComputingUTN.DataAccessLayer;
using CloudComputingUTN.Middleware;
using CloudComputingUTN.Service;
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


builder.Services.AddScoped<IMuseumDbRepository, MuseumDbRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkService, LinkService>();


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
