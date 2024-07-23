using BankSystem.Application.Commands.AccountCommands.DeleteAccountCommand;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;
using BC = BCrypt.Net.BCrypt;


namespace BankSystemBackend.Test;

public class DeleteCommandHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly DeleteCommandHandler _handler;

    public DeleteCommandHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _handler = new DeleteCommandHandler(_uow);
    }

    [Fact]
    public async Task Handle_AccountNotFound_ReturnsFailure()
    {
        // Arrange
        var request = new DeleteCommandRequest { Id = Guid.NewGuid() };
        _uow.BankAccount.GetAccount(request.Id).Returns(Task.FromResult<BankAccount>(null));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_AccountFound_DeletesAccountAndReturnsSuccess()
    {
        // Arrange
        var acID = Guid.NewGuid();
        var account = new BankAccount 
        {
            Id = acID,
            Owner_Name = "test",
            AccountPassword = BC.HashPassword("password123"), // Set to the actual password for the test
            CreditNumber = "1234567812345678", // Example credit number, replace with your logic
            Balance = 1200,
            Cvv2 = "1234", // Example CVV2, replace with your logic
            UserID = Guid.NewGuid()
        };
        var request = new DeleteCommandRequest { Id = acID };
        _uow.BankAccount.GetAccount(request.Id).Returns(Task.FromResult(account));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _uow.BankAccount.Received(1).DeleteAccount(account);
        await _uow.Received(1).Save();

        result.Success.Should().BeTrue();
    }
}