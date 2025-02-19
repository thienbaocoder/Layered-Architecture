using LayeredArchitecture.PersistenceLayer;
using LayeredArchitecture.BusinessLayer.Services;
using Microsoft.Extensions.Options;
using LayeredArchitecture.Models;

var builder = WebApplication.CreateBuilder(args);

// Đọc cấu hình MongoDbSettings từ appsettings.json
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Đăng ký CustomerContext
builder.Services.AddSingleton<CustomerContext>();

// Đăng ký Repository
builder.Services.AddSingleton<CustomerRepository>();

// Đăng ký Service
builder.Services.AddSingleton<CustomerService>();

// Thêm Controllers + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
