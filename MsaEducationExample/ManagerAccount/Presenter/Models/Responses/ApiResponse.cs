using ManagerAccount.UseCases.Dtos;

namespace ManagerAccount.Presenter.Models.Responses;

public class ApiResponse(bool isSuccess, string error, int? errorCode = 0)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public int? ErrorCode { get; set; } = errorCode;
    public string Error{ get; set; } = error;
}

public class ApiResponse<T>(bool isSuccess, string error, int? errorCode, T data) : ApiResponse(isSuccess, error, errorCode)
{
    public T Data { get; set; } = data;
}