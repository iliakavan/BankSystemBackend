using BankSystem.Application.BackgroundTask;
using Hangfire;

namespace BankSystem.Api.BackGroundJobs;



public class RecurringJobsService(ITransactionServiceFactory service) : IHostedService
{
    private readonly ITransactionServiceFactory _service = service;
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var transactionService = _service.CreateTransactionService();

        RecurringJob.AddOrUpdate(
            "process-transactions",
                () => transactionService.CheckAndProcessTransactions(),
                Cron.Minutely());

        await Task.CompletedTask;

    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
