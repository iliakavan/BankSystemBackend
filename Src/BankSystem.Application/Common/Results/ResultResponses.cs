namespace BankSystem.Application.Common.Response;



public class ResultResponses<T>
{
    public bool Success { get; set; }

    public IEnumerable<T>? Result { get; set; }
}
