using BankSystem.Application.BankAccounts.GradeshHesab;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemBackend.Test;

public class GradeshHesabHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly GradeshHesabHandler _handler;

    public GradeshHesabHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _handler = new GradeshHesabHandler(_uow);
    }

    [Fact]
    public async Task Handle_AccountNotAuthenticated_ReturnsFailure()
    {
        // Arrange
        var request = new GradeshHesabRequest("1234567890123456", "password");
        _uow.BankAccount.Authenticate(request.CreditNumber, request.Password).Returns(Task.FromResult<BankAccount>(null));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_AccountAuthenticated_ReturnsTransactions()
    {
        // Arrange
        var request = new GradeshHesabRequest ("1234567890123456", "password");
        var account = new BankAccount { Id = Guid.NewGuid(), CreditNumber = request.CreditNumber, AccountPassword = request.Password , Balance = 1000 , Cvv2 = "123", Owner_Name = "dnsjdjs"};
        var transactions = new List<Transactionhistory>
        {
            new Transactionhistory { Id = 1, OrginCreditNumber = "1234567890123456", DestCreditNumber = "6543210987654321", Amount = 100, Status = BankSystem.Domain.Enum.TransactionStatus.Complete },
            new Transactionhistory { Id = 2, OrginCreditNumber = "1234567890123456", DestCreditNumber = "6543210987654321", Amount = 200, Status = BankSystem.Domain.Enum.TransactionStatus.Pending }
        };

        _uow.BankAccount.Authenticate(request.CreditNumber, request.Password).Returns(Task.FromResult(account));
        _uow.TransactionHistory.GetAllTransactionsOfAccount(account.Id).Returns(await Task.FromResult(transactions));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert

        result.Success.Should().BeTrue();
        result.Result.Should().BeEquivalentTo(transactions);
    }
}