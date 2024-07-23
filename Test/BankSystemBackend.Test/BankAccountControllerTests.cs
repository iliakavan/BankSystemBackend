using BankSystem.Api.Controllers;
using BankSystem.Application.BankAccounts.DipositCommand;
using BankSystem.Application.BankAccounts.FetchAllLoanRequestofAccount;
using BankSystem.Application.BankAccounts.GetAllAccounts;
using BankSystem.Application.BankAccounts.RequestLoan;
using BankSystem.Application.BankAccounts.ShebaTransfer;
using BankSystem.Application.BankAccounts.TransferCommand;
using BankSystem.Application.BankAccounts.WithdrawlCommand;
using BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;
using BankSystem.Application.Commands.AccountCommands.DeleteAccountCommand;
using BankSystem.Application.Common.Response;
using BankSystem.Domain.Models;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System.Collections.Generic;


namespace BankSystemBackend.Test;

public class BankAccountControllerTests
{
    private readonly IMediator _mediator;
    private readonly BankAccountController _controller;

    public BankAccountControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new BankAccountController(_mediator);
    }

    [Fact]
    public async Task CreateAccount_ShouldReturnOk_WhenAccountIsCreatedSuccessfully()
    {
        // Arrange
        var request = new CreateBankAccountCommandRequest 
        { 
            firstname = "nddnjsdn",
            lastname = "njdsndas",
            AccountPassword = BCrypt.Net.BCrypt.HashPassword("1234")
        };
        var resultValue = new ResultResponse() { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.CreateAccount(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task CreateAccount_ShouldReturnBadRequest_WhenAccountCreationFails()
    {
        // Arrange
        var request = new CreateBankAccountCommandRequest 
        {
            firstname = "nddnjsdn",
            lastname = "njdsndas",
            AccountPassword = BCrypt.Net.BCrypt.HashPassword("1234")
        };
        var resultValue = new ResultResponse() { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.CreateAccount(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task DeleteAccount_ShouldReturnOk_WhenAccountIsDeletedSuccessfully()
    {
        // Arrange
        var request = new DeleteCommandRequest
        {
            Id = Guid.NewGuid()
        };
        var resultValue = new ResultResponse { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.DeleteAccount(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task DeleteAccount_ShouldReturnBadRequest_WhenAccountDeletionFails()
    {
        // Arrange
        var request = new DeleteCommandRequest { Id = Guid.NewGuid() };
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.DeleteAccount(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task RequestLoan_ShouldReturnOk_WhenLoanIsRequestedSuccessfully()
    {
        // Arrange
        var request = new BankLoanCommandRequest("sdkjskdjawkjd", "ksdkdkadhkwhd", "kndkawkdawk", 10000); 
        { /* initialize properties */ };
        var resultValue = new ResultResponse() { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.RequestLoan(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task RequestLoan_ShouldReturnBadRequest_WhenLoanRequestFails()
    {
        // Arrange
        var request = new BankLoanCommandRequest("sdkjskdjawkjd", "ksdkdkadhkwhd", "kndkawkdawk", 10000);
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.RequestLoan(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Diposit_ShouldReturnOk_WhenDipositIsSuccessful()
    {
        // Arrange
        var request = new DipositCommandRequest("dsadsdassdasd","sddzsdsdsd",1000);
        var resultValue = new ResultResponse() { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Diposit(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task Diposit_ShouldReturnBadRequest_WhenDipositFails()
    {
        // Arrange
        var request = new DipositCommandRequest("dsadsdassdasd", "sddzsdsdsd", 1000);
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Diposit(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Withdrawl_ShouldReturnOk_WhenWithdrawlIsSuccessful()
    {
        // Arrange
        var request = new WithdrawlCommandRequest("dsadsdassdasd", "sddzsdsdsd", 1000);
        var resultValue = new ResultResponse() { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Withdrawl(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task Withdrawl_ShouldReturnBadRequest_WhenWithdrawlFails()
    {
        // Arrange
        var request = new WithdrawlCommandRequest("dsadsdassdasd", "sddzsdsdsd", 1000);
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Withdrawl(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Transfer_ShouldReturnOk_WhenTransferIsSuccessful()
    {
        // Arrange
        var request = new TransferCommandRequest("dsadsdassdasd","sdsdssd" ,"sddzsdsdsd", 1000);
        var resultValue = new ResultResponse() { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task Transfer_ShouldReturnBadRequest_WhenTransferFails()
    {
        // Arrange
        var request = new TransferCommandRequest("dsadsdassdasd", "sdsdssd", "sddzsdsdsd", 1000);
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Transfer(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }


    [Fact]
    public async Task Shaba_ShouldReturnOk_WhenShabaTransferIsSuccessful()
    {
        // Arrange
        var request = new ShebaTransferCommandRequest() 
        {
            Password = "swmdw",
            Amount = 1000,
            OrginCreditNumber = "dldslddlkds",
            DestCreditNumber = "mdkmdkmkwdwd"
        };
        var resultValue = new ResultResponse { Success = true };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Shaba(request);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task Shaba_ShouldReturnBadRequest_WhenShabaTransferFails()
    {
        // Arrange
        var request = new ShebaTransferCommandRequest
        {
            Password = "swmdw",
            Amount = 1000,
            OrginCreditNumber = "dldslddlkds",
            DestCreditNumber = "mdkmdkmkwdwd"
        };
        var resultValue = new ResultResponse { Success = false };
        _mediator.Send(request).Returns(resultValue);

        // Act
        var result = await _controller.Shaba(request);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}