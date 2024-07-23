using BankSystem.Application.BackgroundServices;
using BankSystem.Application.BankAccounts.TransferCommand;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using FluentAssertions;
using NSubstitute;

public class TransferCommandHandlerTests
{
    private readonly ITransaction _transaction;
    private readonly IUnitOfWork _unitOfWork;
    private readonly TransferCommandHandler _handler;

    public TransferCommandHandlerTests()
    {
        _transaction = Substitute.For<ITransaction>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new TransferCommandHandler(_transaction, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessTrue_WhenTransferIsSuccessful()
    {
        // Arrange
        var request = new TransferCommandRequest("123", "password", "456", 1000);

        _transaction.Transfer(request.creditnumber, request.password, request.creditnumber2, request.amount)
            .Returns(Task.FromResult(new ResultResponse { Success = true }));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenTransferFails()
    {
        // Arrange
        var request = new TransferCommandRequest("123", "password", "456", 1000);

        _transaction.Transfer(request.creditnumber, request.password, request.creditnumber2, request.amount)
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