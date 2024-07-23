using BankSystem.Application.BackgroundServices;
using BankSystem.Application.BackgroundTask;
using BankSystem.Application.CollectTransaction;
using BankSystem.Application.Repositories;
using BankSystem.Application.Services;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using BankSystem.Infrastructure.Data;
using BankSystem.Infrastructure.ExternalServices;
using BankSystem.Infrastructure.Repositories;
using BankSystem.Infrastructure.Services;
using BankSystem.Infrastructure.UnitOfWork.cs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BankSystem.Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });

        services.AddAuthorizationBuilder()
            .AddPolicy("Admin", policy => policy.RequireRole("Admin"))
            .AddPolicy("User", policy => policy.RequireRole("User"));


        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Default"),
            builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddTransient<ITransaction, Transations>();
        services.AddScoped<IUserManagerService, UserManager>();
        services.AddScoped<IUnitOfWork, UnitOfWorks>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ICollectTransactionService, CollectTransactionService>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBankLoanService, BankLoanService>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        //services.AddAutoMapper(typeof(ConfigureService).Assembly);

        return services;
    }
}