using Domain.Enums;
using Domain.Interfaces.Response;
namespace Domain.Response
{
    public class Response<T> : IResponse<T> where T : class
    {
        public int StatusCode { get; }
        public ResponseStatus Status { get; }
        public string Message { get; }
        public T? Data { get; }
        // public bool IsSuccess => Status == ResponseStatus.Ok;
        public bool IsSuccess
        {
            get
            {
                return Status == ResponseStatus.Ok;
            }
        }
        public string? ErrorCode { get; }
        public string? Details { get; }

        public Response(){
            StatusCode = 0;
            Status = ResponseStatus.NotFound;
            Message = "";
            Data = null;
        }
        private Response(ResponseStatus status, string message, T? data = null, string? errorCode = null, string? details = null)
        {
            Status = status;
            Message = message;
            Data = data;
            ErrorCode = errorCode;
            Details = details;
            StatusCode = (int)status; 
        }

        public IResponse<T> Success(T data, string message = "Operation successful") =>
            new Response<T>(ResponseStatus.Ok, message, data);

        public IResponse<T> NotFound(string message = "Resource not found") =>
            new Response<T>(ResponseStatus.NotFound, message);

        public IResponse<T> Error(string message = "An error occurred", string? details = null) =>
            new Response<T>(ResponseStatus.Error, message, null, "500", details);

        public IResponse<T> Unauthorized(string message = "Unauthorized access") =>
            new Response<T>(ResponseStatus.Unauthorized, message);

        public IResponse<T> InvalidInput(string message = "Invalid input data") =>
            new Response<T>(ResponseStatus.InvalidInput, message);

        public IResponse<T> Conflict(string message = "Data conflict") =>
            new Response<T>(ResponseStatus.Conflict, message);

        public IResponse<T> Forbidden(string message = "Operation forbidden") =>
            new Response<T>(ResponseStatus.Forbidden, message);

        public IResponse<T> Timeout(string message = "Operation timed out") =>
            new Response<T>(ResponseStatus.Timeout, message);

        public IResponse<T> Cancelled(string message = "Operation cancelled") =>
            new Response<T>(ResponseStatus.Cancelled, message);

        public IResponse<T> AlreadyExists(string message = "Resource already exists") =>
            new Response<T>(ResponseStatus.AlreadyExists, message);
    }
}
