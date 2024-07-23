namespace BankSystem.Application.Common.Response;



public class ResultResponse
{
    public bool Success { get; set; }

    public string? Massage { get; set; }

}


public class ResultResponse<T>
{
    public bool Success { get; set; }

    public T? Result { get; set; }
}

