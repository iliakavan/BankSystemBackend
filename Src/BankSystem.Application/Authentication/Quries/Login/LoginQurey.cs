using BankSystem.Application.Common.JwtResponse;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Application.Authentication.Qurey.Login;

public class LoginQurey : IRequest<JwtTokenResponse>
{
    public string UsernameOrEmail { get; set; }
    public string Password { get; set; }

}
