using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BankSystem.Application.BackgroundServices;
using BankSystem.Application.BackgroundTask;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;


namespace BankSystemBackend.Test;


public class TransactionServiceTests
{
    private readonly IUnitOfWork _uow;
    private readonly ITransaction _transaction;
    private readonly TransactionService _service;

    public TransactionServiceTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _transaction = Substitute.For<ITransaction>();
        _service = new TransactionService(_uow, _transaction);
    }

    [Fact]
    public async Task CheckAndProcessTransactions_ProcessesAllPendingTransactions()
    {
        // Arrange
        var pendingTransactions = new List<Transactionhistory>
        {
            new Transactionhistory { Id = 1, OrginCreditNumber = "123", DestCreditNumber = "456", Amount = 100, Status = BankSystem.Domain.Enum.TransactionStatus.Pending },
            new Transactionhistory { Id = 2, OrginCreditNumber = "789", DestCreditNumber = "012", Amount = 200, Status = BankSystem.Domain.Enum.TransactionStatus.Pending }
        };

        _uow.TransactionHistory.GetPendingTransactions().Returns(await Task.FromResult(pendingTransactions));
        _transaction.Transfer(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>()).Returns(Task.FromResult(new ResultResponse { Success = true }));

        // Act
        await _service.CheckAndProcessTransactions();

        // Assert
        await _transaction.Received(2).Transfer(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<decimal>());
        _uow.TransactionHistory.Received(2).Update(Arg.Any<Transactionhistory>());
        await _uow.Received(2).Save();
    }

    [Fact]
    public async Task ProcessTransactionAsync_SuccessfulTransaction_SetsStatusToComplete()
    {
        // Arrange
        var transaction = new Transactionhistory { Id = 1, OrginCreditNumber = "123", DestCreditNumber = "456", Amount = 100, Status = BankSystem.Domain.Enum.TransactionStatus.Pending };
        _transaction.Transfer(transaction.OrginCreditNumber, transaction.DestCreditNumber, transaction.Amount).Returns(Task.FromResult(new ResultResponse { Success = true }));

        // Act
        await _service.ProcessTransactionAsync(transaction);

        // Assert
        transaction.Status.Should().Be(BankSystem.Domain.Enum.TransactionStatus.Complete);
        _uow.TransactionHistory.Received(1).Update(transaction);
        await _uow.Received(1).Save();
    }

    [Fact]
    public async Task ProcessTransactionAsync_FailedTransaction_SetsStatusToFailed()
    {
        // Arrange
        var transaction = new Transactionhistory { Id = 1, OrginCreditNumber = "123", DestCreditNumber = "456", Amount = 100, Status = BankSystem.Domain.Enum.TransactionStatus.Pending };
        _transaction.Transfer(transaction.OrginCreditNumber, transaction.DestCreditNumber, transaction.Amount).Returns(Task.FromResult(new ResultResponse { Success = false }));

        // Act
        await _service.ProcessTransactionAsync(transaction);

        // Assert
        transaction.Status.Should().Be(BankSystem.Domain.Enum.TransactionStatus.Failed);
        _uow.TransactionHistory.Received(1).Update(transaction);
        await _uow.Received(1).Save();
    }
}
