namespace BankSystem.Application.Common.CustomExceptions;


public class InvalidCreditNumberException : Exception
{
    public InvalidCreditNumberException(string? massage) : base(massage)
    {
        
    }
}
