using BankSystem.Application.Common.Response;

namespace BankSystem.Application.BackgroundServices;



public interface ITransaction
{
    Task<ResultResponse> Diposit(string creditnumber,string password, decimal amount);

    Task<ResultResponse> Withdrawal(string creditnumber,string password, decimal amount);

    Task<ResultResponse> Transfer(string creditnumber,string password,string creditnumber2, decimal amount);
    Task<ResultResponse> Transfer(string creditnumber, string creditnumber2, decimal amount);
}
