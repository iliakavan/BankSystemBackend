using BankSystem.Application.Authentication.Command.RegisterUser;
using BankSystem.Application.BackgroundTask;
using BankSystem.Application.Common.Behavior;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BankSystem.Application;

public static class ConfigureServices
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        //services.AddValidatorsFromAssembly(typeof(ValidationBehavior<,>).Assembly);
        //services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddSingleton<ITransactionServiceFactory, TransactionServiceFactory>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));
        return services;
    }
}
