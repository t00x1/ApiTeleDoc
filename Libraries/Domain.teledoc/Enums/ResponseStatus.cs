namespace Domain.Enums
{
   
    public enum ResponseStatus
    {
        /// <summary>
        /// Операция успешно выполнена (HTTP 200).
        /// </summary>
        Ok = 200,

        /// <summary>
        /// Ресурс не найден (HTTP 404).
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// Произошла ошибка во время выполнения операции (HTTP 500).
        /// </summary>
        Error = 500,

        /// <summary>
        /// Недостаточно прав для выполнения операции (HTTP 401).
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// Неверные входные данные (HTTP 400).
        /// </summary>
        InvalidInput = 400,

        /// <summary>
        /// Конфликт с существующими данными (HTTP 409).
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// Операция запрещена (HTTP 403).
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// Время ожидания операции истекло (HTTP 408).
        /// </summary>
        Timeout = 408,

        /// <summary>
        /// Операция отменена (Custom HTTP 499).
        /// </summary>
        Cancelled = 499,

        /// <summary>
        /// Ресурс уже существует (HTTP 409).
        /// </summary>
        AlreadyExists = 409
    }
}
