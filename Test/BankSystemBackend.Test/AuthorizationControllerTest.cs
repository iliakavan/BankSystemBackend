using Xunit;
using FluentAssertions;
using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using BankSystem.API.Controllers;
using MediatR;
using System.Threading.Tasks;
using BankSystem.Application.Authentication.Command.RegisterUser;
using BankSystem.Application.Authentication.Qurey.Login;
using BankSystem.Application.Authentication.Command.Logout;
using BankSystem.Application.Authentication.Command.RefreshToken;
using BankSystem.Application.Authentication.Commands.AddUserRole;
using BankSystem.Application.Authentication.Commands.RemoveUserRole;
using System;
using NSubstitute.ExceptionExtensions;
using BankSystem.Application.Common.Response;
using BankSystem.Domain.Models;
using BCrypt.Net;
using BankSystem.Application.Common.JwtResponse;
using Azure.Core;
using BankSystem.Application.Common.CustomException;

namespace BankSystem.Tests.Controllers;

public class AuthorizationControllerTests
{
    private readonly IMediator _mediator;
    private readonly AuthorizationController _controller;

    public AuthorizationControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new AuthorizationController(_mediator);
    }

    [Fact]
    public async Task Registration_ShouldReturnOk_WhenUserIsCreatedSuccessfully()
    {
        // Arrange
        var command = new RegisterUserCommand("iwihdhdihi", "bsdahbdha", "jjasbjasbcj", "ndnjdnsjnj", "Isndjwdn");
        _mediator.Send(command).Returns(new ResultResponse(){ Success = true });

        // Act
        var result = await _controller.Registration(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be("User Created Sucessfully");
    }

    [Fact]
    public async Task Registration_ShouldReturnUnauthorized_WhenUserCreationFails()
    {
        // Arrange
        var command = new RegisterUserCommand("iwihdhdihi","bsdahbdha","jjasbjasbcj","ndnjdnsjnj","Isndjwdn");
        _mediator.Send(command).Returns(new ResultResponse() { Success = false });

        // Act
        var result = await _controller.Registration(command);

        // Assert
        result.Should().BeOfType<UnauthorizedResult>();
    }

    [Fact]
    public async Task Login_ShouldReturnOk_WhenLoginIsSuccessful()
    {
        // Arrange
        JwtTokenResponse token = new JwtTokenResponse("dsdsadada","dsdassdsa");
        var query = new LoginQurey { UsernameOrEmail = "jsbjdbsjdbj",Password = "dksndksnkn" };
        var user = new User() 
        {
            Email = "jsbjdbsjdbj",
            First_Name = "djwbjbdjb",
            Last_Name = "mmskkdksnk",
            Password = BCrypt.Net.BCrypt.HashPassword("dksndksnkn"),
            UserName = "nsdndnjnjn"
        };

        _mediator.Send(query).Returns(token);

        // Act
        var result = await _controller.Login(query);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(token);
    }

    [Fact]
    public async Task Login_ShouldReturnUnauthorized_WhenLoginFails()
    {
        // Arrange
        var query = new LoginQurey { /* initialize properties */ };
        _mediator.Send(query).Throws(new UnauthorizedAccessException());

        // Act
        var result = await _controller.Login(query);

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>();
        (result as UnauthorizedObjectResult).Value.Should().Be("UserName or Password is incorrect");
    }

    [Fact]
    public async Task Logout_ShouldReturnOk_WhenLogoutIsSuccessful()
    {
        // Arrange
        var command = new LogoutUserCommand("bdhdabdjb");
        var resultValue = $"{command.username} Logout Successfully.";
        _mediator.Send(command).Returns(resultValue);

        // Act
        var result = await _controller.Logout(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue);
    }

    [Fact]
    public async Task Refresh_ShouldReturnOk_WhenTokenIsRefreshedSuccessfully()
    {
        // Arrange
        var command = new RefreshTokenCommandRequest("bbahsbhabdbasidjabdjabjbajsb");
        var token = new JwtTokenResponse("newAccessToken","newRefreshToken");
        _mediator.Send(command).Returns(token);

        // Act
        var result = await _controller.Refresh(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(token);
    }

    [Fact]
    public async Task Refresh_ShouldReturnUnauthorized_WhenTokenIsInvalid()
    {
        // Arrange
        var command = new RefreshTokenCommandRequest("Invalid-refreshtoken");
        _mediator.Send(command).Throws(new SecurityTokenException("Refresh Token is Invalid"));

        // Act
        var result = await _controller.Refresh(command);

        // Assert
        result.Should().BeOfType<UnauthorizedObjectResult>();
        (result as UnauthorizedObjectResult).Value.Should().Be("Token is not valid");
    }

    [Fact]
    public async Task AddRole_ShouldReturnOk_WhenRoleIsAddedSuccessfully()
    {
        // Arrange
        var command = new AddUserRoleCommand
        { 
            UserId = Guid.NewGuid(),
            RoleId = 1
        };
        var resultValue = new ResultResponse() { Success = true, Massage = "Role added successfully" };
        _mediator.Send(command).Returns(resultValue);

        // Act
        var result = await _controller.AddRole(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue.Massage);
    }

    [Fact]
    public async Task AddRole_ShouldReturnNotFound_WhenRoleAdditionFails()
    {
        // Arrange
        var command = new AddUserRoleCommand 
        {
            UserId = Guid.NewGuid(),
            RoleId = 1
        };
        var resultValue = new ResultResponse() { Success = false, Massage = "Role not found" };
        _mediator.Send(command).Returns(resultValue);

        // Act
        var result = await _controller.AddRole(command);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        (result as NotFoundObjectResult).Value.Should().Be(resultValue.Massage);
    }

    [Fact]
    public async Task RemoveRole_ShouldReturnOk_WhenRoleIsRemovedSuccessfully()
    {
        // Arrange
        var command = new RemoveUserRoleCommand 
        { 
            Id = 1
        };
        var resultValue = new ResultResponse() { Success = true, Massage = "Role removed successfully" };
        _mediator.Send(command).Returns(resultValue);

        // Act
        var result = await _controller.RemoveRole(command);

        // Assert
        result.Should().BeOfType<OkObjectResult>();
        (result as OkObjectResult).Value.Should().Be(resultValue.Massage);
    }

    [Fact]
    public async Task RemoveRole_ShouldReturnNotFound_WhenRoleRemovalFails()
    {
        // Arrange
        var command = new RemoveUserRoleCommand { 
            Id = 1
        };
        var resultValue = new ResultResponse() { Success = false, Massage = "Role not found" };
        _mediator.Send(command).Returns(resultValue);

        // Act
        var result = await _controller.RemoveRole(command);

        // Assert
        result.Should().BeOfType<NotFoundObjectResult>();
        (result as NotFoundObjectResult).Value.Should().Be(resultValue.Massage);
    }
}
