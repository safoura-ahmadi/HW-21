namespace CarCheckup.Domain.Core.Entities;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public Result(bool isSucces, string? message = null)
    {
        IsSuccess = isSucces;
        Message = message;
    }
}
