using BankSystem.Application.BackgroundServices;
using BankSystem.Application.CollectTransaction;
using BankSystem.Application.Common.CustomExceptions;
using BankSystem.Application.Common.Response;
using BankSystem.Application.Repositories;
using BankSystem.Application.UnitOfWork;


namespace BankSystem.Infrastructure.Services;


public class Transations(IUnitOfWork uow,ICollectTransactionService collect) : ITransaction
{
    private readonly IUnitOfWork _uow = uow;
    private readonly ICollectTransactionService _collect = collect;
    public async Task<ResultResponse> Diposit(string creditnumber, string password, decimal amount)
    {
        var account = await _uow.BankAccount.Authenticate(creditnumber, password);

        if (account == null)
        {
            return new() { Massage = "Credit Number or password is invalid", Success = false };
        }

        account.Balance += amount;

        _uow.BankAccount.UpdateAccount(account);
        await _uow.Save();
        await _collect.CollectTransaction(account.Id, account.CreditNumber," ", amount, "Diposit");
        return new() { Massage = "Opreation was Succefull",Success = true };
    }

    public async Task<ResultResponse> Transfer(string creditnumber, string password, string creditnumber2, decimal amount)
    {
        var account = await _uow.BankAccount.Authenticate(creditnumber, password);
        if (account is null)
        {
            return new() { Massage = "Credit Number or password is invalid", Success =false};
        }

        var account2 = await _uow.BankAccount.GetAccountByCredit_Number(creditnumber2);
        if (account2 is null)
        {
            return new() { Massage = "Credit Number is invalid", Success = false };
        }
        if(account.Balance < amount && account.IsBlocked) 
        {
            return new() { Massage = "Balance is not enough and isBlocked ", Success = false };
        }
        account.Balance -= amount;
        account2.Balance += amount;

        _uow.BankAccount.UpdateAccount(account);
        _uow.BankAccount.UpdateAccount(account2);


        await _uow.Save();
        await _collect.CollectTransaction(account.Id, account.CreditNumber, account2.CreditNumber , amount, "Transfer");
        return new() { Massage = "Opreation was Succefull", Success = true };

    }

    public async Task<ResultResponse> Transfer(string creditnumber, string creditnumber2, decimal amount)
    {
        var account = await _uow.BankAccount.GetAccountByCredit_Number(creditnumber);
        if (account is null)
        {
            return new() { Massage = "Credit Number is invalid", Success = false };
        }

        var account2 = await _uow.BankAccount.GetAccountByCredit_Number(creditnumber2);
        if (account2 is null)
        {
            return new() { Massage = "Credit Number is invalid", Success = false };
        }
        if (account.Balance < amount || account.IsBlocked)
        {
            return new() { Massage = "Insufficient balance or source account is blocked", Success = false };
        }
        if(account.Balance == 0) 
        {
            
            account.IsBlocked = true;
            _uow.BankAccount.UpdateAccount(account);
            return new() { Massage = "Insufficient balance", Success = false };
        }
        if (account2.IsBlocked)
        {
            if (amount < 100)
            {
                return new ResultResponse { Massage = "Destination account is blocked and cannot receive non-100 transfers", Success = false };
            }

            account2.Balance += amount;
            account2.IsBlocked = false;
            _uow.BankAccount.UpdateAccount(account2);
        }

        account.Balance -= amount;
        account2.Balance += amount;

        _uow.BankAccount.UpdateAccount(account);
        _uow.BankAccount.UpdateAccount(account2);


        await _uow.Save();
        await _collect.CollectTransaction(account.Id, account.CreditNumber, account2.CreditNumber, amount, "TransferSheba");
        return new() { Massage = "Opreation was Successfull", Success = true };

    }
    public async Task<ResultResponse> Withdrawal(string creditnumber, string password, decimal amount)
    {
        var account = await _uow.BankAccount.Authenticate(creditnumber, password);

        if (account == null)
        {
            return new() { Massage = "Credit Number or password is invalid", Success = false };
        }
        else if (account != null && account.Balance > amount)
        {
            account.Balance -= amount;
            _uow.BankAccount.UpdateAccount(account);
            await _uow.Save();
            await _collect.CollectTransaction(account.Id, account.CreditNumber, " ", amount, "Withdrawl");
            return new() { Success = true };
        }
        
        return new() { Massage = "Balance is not enough", Success = false };

    }
}

   



