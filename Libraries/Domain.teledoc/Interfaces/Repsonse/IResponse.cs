using Domain.Enums;

namespace Domain.Interfaces.Response
{
    public interface IResponse<T> where T : class
    {
        int StatusCode { get; }
        ResponseStatus Status { get; }
        string Message { get; }
        T? Data { get; }
        bool IsSuccess { get; }
        string? ErrorCode { get; }
        string? Details { get; }

        IResponse<T> Success(T data, string message = "Operation successful");
        IResponse<T> NotFound(string message = "Resource not found");
        IResponse<T> Error(string message = "An error occurred", string? details = null);
        IResponse<T> Unauthorized(string message = "Unauthorized access");
        IResponse<T> InvalidInput(string message = "Invalid input data");
        IResponse<T> Conflict(string message = "Data conflict");
        IResponse<T> Forbidden(string message = "Operation forbidden");
        IResponse<T> Timeout(string message = "Operation timed out");
        IResponse<T> Cancelled(string message = "Operation cancelled");
        IResponse<T> AlreadyExists(string message = "Resource already exists");
    }
}
