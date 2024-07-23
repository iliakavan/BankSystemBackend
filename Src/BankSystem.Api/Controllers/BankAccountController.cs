using BankSystem.Application.BankAccounts.DipositCommand;
using BankSystem.Application.BankAccounts.FetchAllLoanRequestofAccount;
using BankSystem.Application.BankAccounts.GetAllAccounts;
using BankSystem.Application.BankAccounts.GradeshHesab;
using BankSystem.Application.BankAccounts.RequestLoan;
using BankSystem.Application.BankAccounts.ShebaTransfer;
using BankSystem.Application.BankAccounts.TransferCommand;
using BankSystem.Application.BankAccounts.WithdrawlCommand;
using BankSystem.Application.Commands.AccountCommands.CreateAccountCommand;
using BankSystem.Application.Commands.AccountCommands.DeleteAccountCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class BankAccountController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost]

    public async Task<IActionResult> CreateAccount([FromBody] CreateBankAccountCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpDelete]

    public async Task<IActionResult> DeleteAccount([FromBody] DeleteCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("RequestLoan")]

    public async Task<IActionResult> RequestLoan([FromBody] BankLoanCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("Diposit")]

    public async Task<IActionResult> Diposit([FromBody] DipositCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("Withdrawl")]
    public async Task<IActionResult> Withdrawl([FromBody]WithdrawlCommandRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpPost]
    [Route("Transfer")]

    public async Task<IActionResult> Transfer([FromBody]TransferCommandRequest request) 
    {
        var result = await _mediator.Send(request);
        if (!result.Success)
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLoanRequests(Guid id) 
    {
        var qurey = new GetAllAccountLoanRequests { ID = id};

        var result = await _mediator.Send(qurey);

        return Ok(result);
    }

    [HttpGet]

    public async Task<IActionResult> GetAllAccounts() 
    {
        var result = await _mediator.Send(new GetAllAcountsQureyRequest());

        return Ok(result);
    }

    [HttpPost]
    [Route("Shaba")]

    public async Task<IActionResult> Shaba([FromBody] ShebaTransferCommandRequest request) 
    {
        var result = await _mediator.Send(request);
        if (!result.Success) 
        {
            return BadRequest();
        }
        return Ok(result);
    }

    [HttpGet]
    [Route("GardeshHesab")]

    public async Task<IActionResult> GardeshHesab([FromQuery] GradeshHesabRequest request) 
    {
        var result = await _mediator.Send(request);
        if(!result.Success) 
        {
            return BadRequest();
        }
        return Ok(result);
    }

}