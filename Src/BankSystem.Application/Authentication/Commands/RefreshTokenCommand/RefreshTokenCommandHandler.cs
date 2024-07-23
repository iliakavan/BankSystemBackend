using BankSystem.Application.Common.CustomException;
using BankSystem.Application.Common.JwtResponse;
using BankSystem.Application.Services;
using BankSystem.Application.UnitOfWork;
using MediatR;
using System.Security.Claims;

namespace BankSystem.Application.Authentication.Command.RefreshToken;


public class RefreshTokenCommandHandler(IUnitOfWork uow,IUserManagerService userManager,ITokenService tokenService) : IRequestHandler<RefreshTokenCommandRequest, JwtTokenResponse>
{
    private readonly IUserManagerService _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUnitOfWork _uow = uow;
    public async Task<JwtTokenResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.RefreshToken);
        if (principal == null)
            throw new SecurityTokenException("Invalid refresh token");

        var username = principal.Identity.Name;

        var user = await _uow.Users.GetUserByUserName(username);
        if (user == null)
            throw new SecurityTokenException("User not found");

        var refreshToken = await _uow.RefreshToken.GetRefreshToken(user.ID);
        var result = refreshToken.SingleOrDefault(rf => rf.Token == request.RefreshToken);
        if (refreshToken == null || !result.IsActive)
            throw new SecurityTokenException("Refresh Token is Invalid");

        var tokens = await _tokenService.GenerateJwtTokens(user);

        await _userManager.SaveRefreshToken(username, tokens.RefreshToken);

        return new JwtTokenResponse(tokens.AccessToken, tokens.RefreshToken);
    }
}
