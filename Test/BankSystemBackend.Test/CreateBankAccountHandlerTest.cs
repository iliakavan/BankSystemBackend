using BC = BCrypt.Net.BCrypt;
using BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace BankSystemBackend.Test;

public class CreateBankAccountHandlerTests
{
    private readonly IUnitOfWork _uow;
    private readonly CreateBankAccountHandler _handler;

    public CreateBankAccountHandlerTests()
    {
        _uow = Substitute.For<IUnitOfWork>();
        _handler = new CreateBankAccountHandler(_uow);
    }

    [Fact]
    public async Task Handle_NullRequest_ReturnsFailure()
    {
        // Arrange
        CreateBankAccountCommandRequest request = null;

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ValidRequest_CreatesBankAccountAndReturnsSuccess()
    {
        // Arrange
        var request = new CreateBankAccountCommandRequest
        {
            firstname = "John",
            lastname = "Doe",
            AccountPassword = "password123",
            Balance = 1000,
            UserId = Guid.NewGuid()
        };

        var expectedAccount = new BankAccount
        {
            Owner_Name = $"{request.firstname} {request.lastname}",
            AccountPassword = BC.HashPassword("password123"), // Set to the actual password for the test
            CreditNumber = "1234567812345678", // Example credit number, replace with your logic
            Balance = request.Balance,
            Cvv2 = "1234", // Example CVV2, replace with your logic
            UserID = request.UserId
        };

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        await _uow.BankAccount.Received(1).CreateAccount(Arg.Is<BankAccount>(a =>
            a.Owner_Name == $"{request.firstname} {request.lastname}" &&
            a.Balance == request.Balance &&
            a.Cvv2.Length == 4 &&
            a.CreditNumber.Length == 16 &&
            a.UserID == request.UserId));

        await _uow.Received(1).Save();

        result.Success.Should().BeTrue();
    }
}