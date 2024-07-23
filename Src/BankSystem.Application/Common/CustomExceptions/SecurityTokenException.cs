namespace BankSystem.Application.Common.CustomException;

public class SecurityTokenException : Exception
{
    public SecurityTokenException(string massage) : base(massage)
    {
        
    }
}
