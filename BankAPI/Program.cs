using BankAPI.Data;
using BankAPI.Services.Accounts;
using BankAPI.Services.Customers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddAutoMapper(typeof(Program).Assembly);
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = """Standard Authorization header using the bearer scheme. Example: "bearer {token}" """,
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        }); 

        c.OperationFilter<SecurityRequirementsOperationFilter>(); 

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
        o => o.UseNpgsql(builder.Configuration.GetConnectionString("BankAPI")));
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),
                ValidateIssuer = false, 
                ValidateAudience = false
            }; 
        }); 
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
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}