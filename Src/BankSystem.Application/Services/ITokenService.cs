using BankSystem.Application.Common.JwtResponse;
using BankSystem.Domain.Models;
using System.Security.Claims;

namespace BankSystem.Application.Services;


public interface ITokenService
{
    Task<JwtTokenResponse> GenerateJwtTokens(User user);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
