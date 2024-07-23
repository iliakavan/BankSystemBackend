namespace BankSystem.Application.Common.CustomExceptions;



public class AccountAuthenticationException : Exception
{
    public AccountAuthenticationException(string massage) : base(massage)
    {
        
    }
}
