using BankSystem.Application.Authentication.Command.Logout;
using BankSystem.Application.Authentication.Command.RefreshToken;
using BankSystem.Application.Authentication.Command.RegisterUser;
using BankSystem.Application.Authentication.Commands.AddUserRole;
using BankSystem.Application.Authentication.Commands.RemoveUserRole;
using BankSystem.Application.Authentication.Qurey.Login;
using BankSystem.Application.Common.CustomException;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.API.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorizationController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;
    [HttpPost]
    [Route("Register")]

    public async Task<IActionResult> Registration(RegisterUserCommand registerUser) 
    {
        var user = await _mediator.Send(registerUser);
        if (!user.Success) 
        {
            return Unauthorized();
        }

        return Ok("User Created Sucessfully");
    }

    [HttpPost]
    [Route("Login")]

    public async Task<IActionResult> Login(LoginQurey UserLoginRequest) 
    {
        try
        {
            var user = await _mediator.Send(UserLoginRequest);
            return Ok(user);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("UserName or Password is incorrect");
        }
        
    }

    [HttpPost]
    [Route("Logout")]

    public async Task<IActionResult> Logout(LogoutUserCommand logoutUser) 
    {
        var result = await _mediator.Send(logoutUser);
        return Ok(result);
    }

    [HttpPost]
    [Route("Refresh")]

    public async Task<IActionResult> Refresh(RefreshTokenCommandRequest refresh) 
    {
        try
        {
            var token = await _mediator.Send(refresh);
            return Ok(token);
        }
        catch(SecurityTokenException) 
        {
            return Unauthorized("Token is not valid");
        }
    }
    [HttpPost]
    [Route("/Roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddRole([FromBody]AddUserRoleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return NotFound(result.Massage);
        }
        return Ok(result.Massage);
    }

    [HttpDelete]
    [Route("/Roles")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RemoveRole([FromBody]RemoveUserRoleCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result.Success)
        {
            return NotFound(result.Massage);
        }
        return Ok(result.Massage);
    }
}

