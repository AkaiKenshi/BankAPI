using BankAPI.Data;
using BankAPI.Services.Accounts;
using BankAPI.Services.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Bank API",
            Description = "An API to manage a fictitious Bank for a University Project",
            Version = "v0.1.23420a"
        });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

        c.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<IAccountService, AccountService>();
    builder.Services.AddDbContext<BankDataContext>(
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("BankAPIClass"))
        );
}

var app = builder.Build();
{
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
}