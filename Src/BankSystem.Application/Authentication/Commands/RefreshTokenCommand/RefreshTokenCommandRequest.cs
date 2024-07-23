using BankSystem.Application.Common.JwtResponse;
using MediatR;

namespace BankSystem.Application.Authentication.Command.RefreshToken;

public record RefreshTokenCommandRequest(string RefreshToken) : IRequest<JwtTokenResponse>;
