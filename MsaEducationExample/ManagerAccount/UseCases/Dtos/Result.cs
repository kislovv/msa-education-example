namespace ManagerAccount.UseCases.Dtos;

public class Result
{
    public bool IsSuccess { get; set; }
    public int? ErrorCode { get; set; }
    public string Error{ get; set; }
}

public class Result<T> : Result
{
    public T Data { get; set; }
}