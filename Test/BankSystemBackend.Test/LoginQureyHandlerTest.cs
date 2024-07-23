using BankSystem.Application.Authentication.Qurey.Login;
using BankSystem.Application.Common.JwtResponse;
using BankSystem.Application.Services;
using BankSystem.Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace BankSystemBackend.Test;

public class LoginQureyHandlerTests
{
    private readonly IUserManagerService _userManagerService;
    private readonly ITokenService _tokenService;
    private readonly LoginQureyHandler _handler;

    public LoginQureyHandlerTests()
    {
        _userManagerService = Substitute.For<IUserManagerService>();
        _tokenService = Substitute.For<ITokenService>();
        _handler = new LoginQureyHandler(_userManagerService, _tokenService);
    }

    [Fact]
    public async Task Handle_ValidCredentials_ShouldReturnJwtTokenResponse()
    {
        // Arrange
        var request = new LoginQurey { UsernameOrEmail = "test@example.com", Password = "password" };
        var user = new User { UserName = "testuser" ,Email = "test@example.com",Password = "password",First_Name = "test" ,Last_Name = "User"};
        var tokens = new JwtTokenResponse("token", "refresh_token");

        _userManagerService.Authenticate(request.UsernameOrEmail, request.Password).Returns(Task.FromResult(user));
        _tokenService.GenerateJwtTokens(user).Returns(Task.FromResult(tokens));

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.AccessToken.Should().Be(tokens.AccessToken);
        result.RefreshToken.Should().Be(tokens.RefreshToken);

        await _userManagerService.Received(1).Authenticate(request.UsernameOrEmail, request.Password);
        await _tokenService.Received(1).GenerateJwtTokens(user);
        await _userManagerService.Received(1).SaveRefreshToken(user.UserName, tokens.RefreshToken);
    }

    [Fact]
    public async Task Handle_InvalidCredentials_ShouldThrowUnauthorizedAccessException()
    {
        // Arrange
        var request = new LoginQurey { UsernameOrEmail = "test@example.com", Password = "wrongpassword" };

        _userManagerService.Authenticate(request.UsernameOrEmail, request.Password).Returns(Task.FromResult<User>(null));

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<UnauthorizedAccessException>();

        await _userManagerService.Received(1).Authenticate(request.UsernameOrEmail, request.Password);
        await _tokenService.DidNotReceive().GenerateJwtTokens(Arg.Any<User>());
        await _userManagerService.DidNotReceive().SaveRefreshToken(Arg.Any<string>(), Arg.Any<string>());
    }
}
