using BankSystem.Application.BackgroundServices;
using BankSystem.Application.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.BackgroundTask;

public interface ITransactionServiceFactory
{
    ITransactionService CreateTransactionService();
}

public class TransactionServiceFactory : ITransactionServiceFactory
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public TransactionServiceFactory(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public ITransactionService CreateTransactionService()
    {
        var scope = _serviceScopeFactory.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ITransactionService>();
    }
}
