using BankSystem.Application.Common.JwtResponse;
using BankSystem.Application.Services;
using MediatR;

namespace BankSystem.Application.Authentication.Qurey.Login;


public class LoginQureyHandler(
                IUserManagerService userManagerService,
                ITokenService tokenService
        ) : IRequestHandler<LoginQurey, JwtTokenResponse>
{
    private readonly IUserManagerService _userService = userManagerService;
    private readonly ITokenService _tokenService = tokenService;
    public async Task<JwtTokenResponse> Handle(LoginQurey request, CancellationToken cancellationToken)
    {
        var user = await _userService.Authenticate(request.UsernameOrEmail, request.Password);
        if (user == null)
            throw new UnauthorizedAccessException();

        var tokens = await _tokenService.GenerateJwtTokens(user);

        await _userService.SaveRefreshToken(user.UserName, tokens.RefreshToken);

        return new JwtTokenResponse(tokens.AccessToken, tokens.RefreshToken);

    }
}

