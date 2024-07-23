using BankSystem.Application.Common.JwtResponse;
using BankSystem.Application.Services;
using BankSystem.Application.UnitOfWork;
using BankSystem.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankSystem.Infrastructure.ExternalServices;


public sealed class TokenService(IConfiguration config,IUnitOfWork uow) : ITokenService
{
    private readonly IConfiguration _config = config;
    private readonly IUnitOfWork _uow = uow;
    public async Task<JwtTokenResponse> GenerateJwtTokens(User user)
    {
        var AccessToken = await GenerateAccessToken(user);
        var RefreshToken = await GenerateRefreshToken(user);

        return new(AccessToken, RefreshToken);
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
            ValidateLifetime = false, // Here we are saying that we don't care about the token's expiration date
            ValidIssuer = _config["Jwt:Issuer"],
            ValidAudience = _config["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }

    private async Task<string> GenerateAccessToken(User user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
        claims.Add(new Claim(ClaimTypes.Email, user.Email));

        var Roles = await _uow.Users.GetRole(user.ID);
        foreach (Role role in Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    private async Task<string> GenerateRefreshToken(User user)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        var Roles = await _uow.Users.GetRole(user.ID);
        foreach (Role role in Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Name));
        }

        var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);

    }


}