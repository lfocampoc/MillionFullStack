namespace RealEstateAPI.Core.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;

        public static ApiResponse<T> SuccessResult(T data, string message = "Operación exitosa")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                Error = string.Empty
            };
        }

        public static ApiResponse<T> ErrorResult(string error, string message = "Error en la operación")
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Message = message,
                Error = error
            };
        }
    }
}
