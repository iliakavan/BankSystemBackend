using BankSystem.Application.BackgroundServices;
using BankSystem.Application.BankAccounts.DipositCommand;
using BankSystem.Application.Common.Response;
using BankSystem.Application.UnitOfWork;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemBackend.Test;

public class DipositCommandHandlerTests
{
    private readonly ITransaction _transaction;
    private readonly IUnitOfWork _unitOfWork;
    private readonly DipositCommandHandler _handler;

    public DipositCommandHandlerTests()
    {
        _transaction = Substitute.For<ITransaction>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _handler = new DipositCommandHandler(_transaction, _unitOfWork);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessTrue_WhenDipositIsSuccessful()
    {
        // Arrange
        var request = new DipositCommandRequest("123", "password", 1000);


        _transaction.Diposit(request.creditnumber, request.password, request.amount)
            .Returns(Task.FromResult(new ResultResponse() { Success = true }));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessFalse_WhenDipositFails()
    {
        // Arrange
        var request = new DipositCommandRequest("123", "password", 1000);


        _transaction.Diposit(request.creditnumber, request.password, request.amount)
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
