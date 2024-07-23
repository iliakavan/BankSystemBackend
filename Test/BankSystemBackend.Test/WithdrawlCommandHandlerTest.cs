using BankSystem.Application.BackgroundServices;
using BankSystem.Application.BankAccounts.WithdrawlCommand;
using BankSystem.Application.Common.Response;
using FluentAssertions;
using NSubstitute;

namespace BankSystemBackend.Test;


public class WithdrawlCommandHandlerTests
{
    private readonly ITransaction _transaction;
    private readonly WithdrawlCommandHandler _handler;

    public WithdrawlCommandHandlerTests()
    {
        _transaction = Substitute.For<ITransaction>();
        _handler = new WithdrawlCommandHandler(_transaction);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessTrue_WhenWithdrawalIsSuccessful()
    {
        // Arrange
        var request = new WithdrawlCommandRequest("123", "password", 1000);

        _transaction.Withdrawal(request.creditnumber, request.password, request.amount)
            .Returns(Task.FromResult(new ResultResponse() { Success = true }));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenWithdrawalFails()
    {
        // Arrange
        var request = new WithdrawlCommandRequest("123", "password", 1000);
     

        _transaction.Withdrawal(request.creditnumber, request.password, request.amount)
            .Returns(Task.FromResult(new ResultResponse() { Success = false }));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenRequestIsNull()
    {
        // Act
        var result = await _handler.Handle(null, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }
}
