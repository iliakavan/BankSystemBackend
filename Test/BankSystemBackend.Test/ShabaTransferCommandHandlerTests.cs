using System;
using System.Threading;
using System.Threading.Tasks;
using BankSystem.Application.BankAccounts.ShebaTransfer;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

public class ShabaTransferCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ShabaTransferCommandHandler _handler;

    public ShabaTransferCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new ShabaTransferCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessTrue_WhenEveryConditionIsCorrect()
    {
        // Arrange
        var request = new ShebaTransferCommandRequest
        {
            OrginCreditNumber = "123",
            Password = "password",
            DestCreditNumber = "456",
            Amount = 1000
        };

        var originAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            CreditNumber = "4567",
            Owner_Name = "John Test",
            Balance = 1000,
            AccountPassword = "password123",
            Cvv2 = "1234"

        };

        var destinationAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            CreditNumber = "456",
            Owner_Name = "John Doe",
            Balance = 1000,
            AccountPassword = "password123",
            Cvv2 = "123"
        };

        _unitOfWork.BankAccount.Authenticate(request.OrginCreditNumber, request.Password).Returns(Task.FromResult(originAccount));
        _unitOfWork.BankAccount.GetAccountByCredit_Number(request.DestCreditNumber).Returns(Task.FromResult(destinationAccount));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        await _unitOfWork.TransactionHistory.Received(1).CreateTransactionHistory(Arg.Is<Transactionhistory>(t =>
            t.AccountID == originAccount.Id &&
            t.Amount == request.Amount &&
            t.DestCreditNumber == request.DestCreditNumber &&
            t.Status == BankSystem.Domain.Enum.TransactionStatus.Pending &&
            t.OrginCreditNumber == request.OrginCreditNumber
        ));
        await _unitOfWork.Received(1).Save();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenOriginAccountIsBlocked()
    {
        // Arrange
        var request = new ShebaTransferCommandRequest
        {
            OrginCreditNumber = "123",
            Password = "password",
            DestCreditNumber = "456",
            Amount = 1000
        };

        var originAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            CreditNumber = "456",
            Owner_Name = "John Doe",
            Balance = 0,
            AccountPassword = "password123",
            Cvv2 = "123",
            IsBlocked = true
        };
        var destinationAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            CreditNumber = "45678",
            Owner_Name = "John Doe",
            Balance = 1000,
            AccountPassword = "password123",
            Cvv2 = "1234"
        };

        _unitOfWork.BankAccount.Authenticate(request.OrginCreditNumber, request.Password).Returns(await Task.FromResult(originAccount));
        _unitOfWork.BankAccount.GetAccountByCredit_Number(request.DestCreditNumber).Returns(await Task.FromResult(destinationAccount));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        await _unitOfWork.TransactionHistory.DidNotReceive().CreateTransactionHistory(Arg.Any<Transactionhistory>());
        await _unitOfWork.DidNotReceive().Save();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenDestinationAccountIsNull()
    {
        // Arrange
        var request = new ShebaTransferCommandRequest
        {
            OrginCreditNumber = "123",
            Password = "password",
            DestCreditNumber = "456",
            Amount = 1000
        };

        var originAccount = new BankAccount
        {
            Id = Guid.NewGuid(),
            CreditNumber = "456",
            Owner_Name = "John Doe",
            Balance = 1000,
            AccountPassword = "password123",
            Cvv2 = "123"
        };

        _unitOfWork.BankAccount.Authenticate(request.OrginCreditNumber, request.Password).Returns(Task.FromResult(originAccount));
        _unitOfWork.BankAccount.GetAccountByCredit_Number(request.DestCreditNumber).Returns(Task.FromResult<BankAccount>(null));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        await _unitOfWork.TransactionHistory.DidNotReceive().CreateTransactionHistory(Arg.Any<Transactionhistory>());
        await _unitOfWork.DidNotReceive().Save();
    }
}
