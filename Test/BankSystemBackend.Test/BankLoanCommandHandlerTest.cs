using BankSystem.Application.BankAccounts.RequestLoan;
using BankSystem.Application.Common.Response;
using BankSystem.Application.Services;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystemBackend.Test;

public class BankLoanCommandHandlerTests
{
    private readonly IBankLoanService _loanService;
    private readonly BankLoanCommandHandler _handler;

    public BankLoanCommandHandlerTests()
    {
        _loanService = Substitute.For<IBankLoanService>();
        _handler = new BankLoanCommandHandler(_loanService);
    }

    [Fact]
    public async Task Handle_RequestLoanSuccess_ReturnsSuccessResponse()
    {
        // Arrange
        var request = new BankLoanCommandRequest("testuser", "password123", "1234567890123456", 1000);

        var loanResult = new ResultResponse()
        {
            Success = true,
            Massage = "Loan approved"
        };

        _loanService.RequestLoan(request.username, request.password, request.CreditNumber, request.RequestedAmountOfloan)
            .Returns(Task.FromResult(loanResult));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Massage.Should().Be("Loan approved");
    }

    [Fact]
    public async Task Handle_RequestLoanFailure_ReturnsFailureResponse()
    {
        // Arrange
        var request = new BankLoanCommandRequest("testuser", "wrongpassword", "1234567890123456", 1000);

        var loanResult = new ResultResponse()
        {
            Success = false,
            Massage = "Loan denied"
        };

        _loanService.RequestLoan(request.username, request.password, request.CreditNumber, request.RequestedAmountOfloan)
            .Returns(Task.FromResult(loanResult));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Massage.Should().Be("Loan denied");
    }
}
